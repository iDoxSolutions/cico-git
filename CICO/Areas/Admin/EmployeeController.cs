using System;
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
        public List<EmployeeAccess> EmployeeAccess { get; set; }
        public string SelectedProxy { get; set; }
        public bool EditEnabled { get; set; }
        public void Load(ICicoContext db)
        {
            Proxies = db.Staffs.Include("SystemRoles").Where(c => c.SystemRoles.Any(d => d.Name == SystemRole.UserProxy)).ToList().Select(c => new SelectListItem() { Text = c.UserId, Value = c.UserId }).ToList();
            if (Employee != null && Employee.Proxy != null)
            {
                SelectedProxy = Employee.Proxy.UserId;
            }
            EmployeeAccess = AddEmployeeAccess(db);
        }

        private List<EmployeeAccess> AddEmployeeAccess(ICicoContext db)
        {
            var fieldNames = typeof(Employee).GetProperties().Select(a => new SelectListItem() { Text = a.ToString(), Value = a.Name, }).ToList().OrderBy(a => a.Text);
            var EmployeeAccessModel = new List<EmployeeAccess>();

            for (var i = 0; i < fieldNames.Count(); i++)
            {
                var fieldName = fieldNames.ToArray()[i];
                var employeeRow = db.EmployeeAccess.SingleOrDefault(e => e.Name == fieldName.Value);
                if (employeeRow == null)
                {
                    EmployeeAccessModel.Add(new EmployeeAccess()
                    {
                        Id = i,
                        Name = fieldName.Value,
                        Office0 = "Hide",
                        Office1 = "Hide",
                        Office2 = "Hide",
                        Office3 = "Hide",
                        Office4 = "Hide",
                        Office5 = "Hide",
                        Office6 = "Hide",
                        Office7 = "Hide",
                        Office8 = "Hide",
                        Office9 = "Hide",
                        Office10 = "Hide",
                        Office11 = "Hide",
                        Office12 = "Hide"
                    });
                }
                else
                {
                    EmployeeAccessModel.Add(employeeRow);
                }


            }
            return EmployeeAccessModel;
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
            var model = new EmployeeModel() { Employee = employee };
            var staff = UserSession.GetCurrentStaff();
            if (staff.Office != null)
            {
               // model.UserAccessRights = Db.AccessRights.Where(a => a.Office.Name == staff.Office.Name).ToList();
            }
            model.Load(Db);
            model.EditEnabled = false;
            return View(model);
        }

        [HttpPost]
        public ActionResult Details(EmployeeModel model)
        {
            Employee employee = Db.Employees.Include("CheckListSessions").Include("CheckListSessions.ChecklistTemplate").Single(c => c.Id == model.Employee.Id);
            model.Employee = employee;
            if (!employee.TourEndDate.HasValue)
            {
                ModelState.AddModelError("", "Departure Date is required");
                
                return View(model);
            }
            else
            {
                UserSession.InitCheckOutSession(employee);
                return RedirectToAction("Details",new{id=model.Employee.Id});
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
            var model = new EmployeeModel() { Employee = employee };
            var staff = UserSession.GetCurrentStaff();
            //if (staff.Office != null)
            //{
            //    model.UserAccessRights = Db.AccessRights.Where(a => a.Office.Name == staff.Office.Name).ToList();
            //}
            model.EditEnabled = true;
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
                ModelState.AddModelError("", "You can't delete an employee that has an active process");
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