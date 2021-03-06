﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;
using PagedList;

namespace Cico.Areas.Admin
{ 
    public class EmployeeIndexModel
    {
        public IPagedList<Employee> Employees { get; set; }
        public string SearchString { get; set; }
        public int? Page { get; set; }
        public bool EditEnabled{get; set; }

        public void Requery(ICicoContext db)
        {
            this.Page = Page ?? 1;
            IQueryable<Employee> emps = db.Employees;
            if (!string.IsNullOrEmpty(this.SearchString))
            {
               emps= emps.Where(s => s.FirstName.ToUpper().Contains(SearchString.ToUpper())
                                       || s.LastName.ToUpper().Contains(SearchString.ToUpper()));
            }
            emps = emps.Where(c => c.Active);
            emps=emps.OrderByDescending(c => c.ArrivalDate);
            
            Employees = emps.ToPagedList(Page.Value, 50);
        }
       
    }

    public class EmployeeModel
    {
        public Employee Employee { get; set; }
        public IList<SelectListItem> Proxies { get; set; }
        public string SelectedProxy { get; set; }
        public void Load(ICicoContext db)
        {
            Proxies = db.Staffs.Include("SystemRoles").Where(c=>c.SystemRoles.Any(d=>d.Name==SystemRole.UserProxy)).ToList().Select(c => new SelectListItem() {Text = c.UserId,Value = c.UserId}).ToList();
            if (Employee != null && Employee.Proxy != null)
            {
                SelectedProxy = Employee.Proxy.UserId;
            }
        }
    }

    public class EmployeeController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Admin/Employees/

        public ViewResult Index(EmployeeIndexModel model)
        {
            model.Requery(Db);
            bool edit = false;
            if (User.IsInRole(SystemRole.OfficeAdmin))
            {
                var staff = UserSession.GetCurrentStaff();
                if (staff.Office.Name == "HR")
                {
                    edit = true;
                }
            }
            model.EditEnabled = edit || User.IsInRole(SystemRole.GlobalAdmin);
            
            return View(model);
        }

        public ViewResult DependentIndex(int? id) {
            Employee employee = Db.Employees.Find(id);
            return View(employee);
        }

        //
        // GET: /Admin/Employees/Details/5

        public ViewResult Details(int id)
        {
            Employee employee = Db.Employees.Include("CheckListSessions").Include("CheckListSessions.ChecklistTemplate").Single(c => c.Id == id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Details(Employee model)
        {
            Employee employee = Db.Employees.Include("CheckListSessions").Include("CheckListSessions.ChecklistTemplate").Single(c => c.Id == model.Id);
            if (!employee.TourEndDate.HasValue)
            {
                ModelState.AddModelError("", "Departure Date is required");
                return View(employee);
            }
            else
            {
                UserSession.InitCheckOutSession(employee);
                return RedirectToAction("Details",new{id=model.Id});
            }
            
        }


       [HttpPost]
        public ActionResult StartCheckin(Employee model)
       {
           return RedirectToAction("initialize", "home", new { area = "", employeeId =model.Id});
       }

        //
        // GET: /Admin/Employees/Create

        public ActionResult Create()
        {
            var model = new EmployeeModel(){Employee = new Employee()};
            model.Load(Db);
            return View(model);
        } 


        private void AddProxyOptions()
        {
            
        }

        //
        // POST: /Admin/Employees/Create

        [HttpPost]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.SelectedProxy))
                {
                    model.Employee.Proxy = Db.Staffs.Find(model.SelectedProxy);
                }
                Db.Employees.Add(model.Employee);
                Db.SaveChanges();
                return RedirectToAction("Index");  
            }
            model.Load(Db);
            return View(model);
        }
        
        //
        // GET: /Admin/Employees/Edit/5
 
        public ActionResult Edit(int id)
        {
            Employee employee = Db.Employees.Find(id);
            var model = new EmployeeModel() {Employee = employee};
            model.Load(Db);
            return View(model);
        }


        //
        // POST: /Admin/Employees/Edit/5

        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            var employeee = Db.Employees.Single(c=>c.Id ==  model.Employee.Id);
            SecurityGuard.CanEditEmployee(employeee, ModelState);
            if (ModelState.IsValid)
            {
                var checklist = Db.CheckListSessions.Include("CheckListTemplate").FirstOrDefault(c => c.Employee.Id == model.Employee.Id && c.Active);
                if (employeee.ArrivalDate.HasValue && model.Employee.ArrivalDate.HasValue &&
                    employeee.ArrivalDate.Value.Date != model.Employee.ArrivalDate.Value.Date)
                {
                    if (checklist != null)
                    {
                        if (checklist.CheckListTemplate.Type.ToUpper() == "CHECKIN")
                        {
                            checklist.ReferenceDate = model.Employee.ArrivalDate.Value;
                        }

                    }
                }

                if (employeee.TourEndDate.HasValue && model.Employee.TourEndDate.HasValue &&
                    employeee.TourEndDate.Value.Date != model.Employee.TourEndDate.Value.Date)
                {
                    if (checklist.CheckListTemplate.Type.ToUpper() == "CHECKOUT")
                    {
                        checklist.ReferenceDate = model.Employee.TourEndDate.Value;
                    }
                }

                CopyValues(model.Employee,employeee);
                if (!string.IsNullOrEmpty(model.SelectedProxy))
                {
                    var staff = Db.Staffs.Find(model.SelectedProxy);
                    employeee.Proxy = staff;
                    //model.Employee.Proxy = new Staff() {UserId = model.SelectedProxy};
                }
                else
                {
                    employeee.Proxy = null;
                    Db.Entry(employeee).Reference(c => c.Proxy).CurrentValue = null;
                }
                
                
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            model.Load(Db);
            return View(model);
        }

        //
        // GET: /Admin/Employees/Delete/5
 
        public ActionResult Delete(int id)
        {
            Employee employee = Db.Employees.Find(id);
            return View(employee);
        }

        //
        // POST: /Admin/Employees/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Employee employee = Db.Employees.Find(id);
               // Db.Employees.Remove(employee);
                employee.Active = false;
                foreach (var checkListSession in employee.CheckListSessions)
                {
                    checkListSession.Active = false;
                }
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                DontSave = true;
                ModelState.AddModelError("", "You can't delete employee it's assigned");
                return View();
            }
        }

        

        //protected override void Dispose(bool disposing)
        //{
        //    Db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}