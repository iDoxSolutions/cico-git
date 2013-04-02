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
        public void Requery(ICicoContext db)
        {
            this.Page = Page ?? 1;
            IQueryable<Employee> emps = db.Employees;
            if (!string.IsNullOrEmpty(this.SearchString))
            {
               emps= emps.Where(s => s.GivenName.ToUpper().Contains(SearchString.ToUpper())
                                       || s.Surname.ToUpper().Contains(SearchString.ToUpper()));
            }
            emps=emps.OrderByDescending(c => c.ArrivalDate);
            
            Employees = emps.ToPagedList(Page.Value, 50);
        }
       
    }

    public class EmployeeController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Admin/Employees/

        public ViewResult Index(EmployeeIndexModel model)
        {
            model.Requery(Db);
            
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
            Employee employee = Db.Employees.Find(id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Details(Employee model)
        {
            Employee employee = Db.Employees.Find(model.Id);
            if (!employee.TourEndDate.HasValue)
            {
                ModelState.AddModelError("", "Tour End Date is required");
                return View(employee);
            }
            else
            {
                UserSession.InitCheckOutSession(employee);
                return RedirectToAction("index", "home",new{area=""});
            }
            
        }
        //
        // GET: /Admin/Employees/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Employees/Create

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Db.Employees.Add(employee);
                Db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(employee);
        }
        
        //
        // GET: /Admin/Employees/Edit/5
 
        public ActionResult Edit(int id)
        {
            Employee employee = Db.Employees.Find(id);
            return View(employee);
        }

        //
        // POST: /Admin/Employees/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(employee).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
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
            Employee employee = Db.Employees.Find(id);
            Db.Employees.Remove(employee);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        

        //protected override void Dispose(bool disposing)
        //{
        //    Db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}