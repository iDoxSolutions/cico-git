using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cico.Models;
using Cico.Areas.Admin;
using Cico.Models.Helpers;


namespace Cico.Controllers
{
    public class DependentModel
    {
        public Dependent Dependent { get; set; }
        public int EmployeeId { get; set; }
        public DependentAccess DependentAccess { get; set; }
        public int ChecklistId{get; set; }
        public bool EditEnabled { get; set; }
    }

    public class DependentController : ControllerBase
    {
        //
        // GET: /Dependent/

        public ActionResult Index()
        {
            var employee = UserSession.GetCurrent().Employee;
            return View(employee.Dependents);
        }

        public ActionResult Create(int id)
        {
            var employee = Db.Employees.Single(c => c.Id == id);
            
            var checklist = Db.CheckListSessions.FirstOrDefault(c => c.Employee.Id == id && c.Active);
            var staff = UserSession.GetCurrentStaff();
            
            var model = new DependentModel() { Dependent = new Dependent() 
                                               { 
                                                 Employee = employee, SameECData = true 
                                               }, 
                                               EmployeeId = employee.Id, 
                                               ChecklistId = checklist.Id 
                                             };

            if (staff.Office != null)
            {
                //model.UserAccessRights = Db.AccessRights.Where(a => a.Office.Name == staff.Office.Name).ToList();
            }
            return View(model);
        }


       

        [HttpPost]
        public ActionResult Create(DependentModel model)
        {
            var employee = Db.Employees.Single(c => c.Id == model.EmployeeId);
            SecurityGuard.CanEditEmployee(employee, ModelState);
            if (ModelState.IsValid)
            {
                model.Dependent.Employee = Db.Employees.Single(c => c.Id == model.EmployeeId);
                Db.Dependents.Add(model.Dependent);
                Db.SaveChanges();
                return RedirectToAction("index", "home", new { tab = "dependents", id = GetSessionByEmployee(model.Dependent.Employee),land="false" });
            }
            else
            {
                return View(model);
            }

        }

        public ActionResult Edit(int id)
        {
            var dependent = Db.Dependents.Single(c => c.Id == id);
            
            var checklist = Db.CheckListSessions.FirstOrDefault(c => c.Employee.Id == dependent.Employee.Id && c.Active);
            var model = new DependentModel() { Dependent = dependent, EmployeeId = dependent.Employee.Id };
            if (checklist != null)
            {
                model.ChecklistId = checklist.Id;
            }
            
            return View(model);
        }
        public void Validate(DependentModel employee)
        {
            try
            {
                var session = UserSession.GetCurrent();
                
            }
            catch (Exception e)
            {
                var staff = UserSession.GetCurrentStaff();
                if (staff == null)
                {
                    ModelState.AddModelError("","Permission denied");   
                }
                if (UserSession.IsOfficeAdmin && staff.Office.Name != "HR")
                {
                    ModelState.AddModelError("", "Permission denied");   
                }
            }

        }
        [HttpPost]
        public ActionResult Edit(DependentModel model) {
            var dependent = Db.Dependents.Single(c => c.Id == model.Dependent.Id);
            SecurityGuard.CanEditDependent(dependent, ModelState);
            if (ModelState.IsValid)
            {
                
                CopyValues(model.Dependent,dependent);
                Db.Entry(dependent).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("index", "home", new { tab = "dependents",id=GetSessionByEmployee(dependent.Employee),land="false" });
            }
            else
            {
                return View(model);
            }
        }

        // GET: /Admin/Office/Delete/5

        public ActionResult Delete(int id) {
            var dependent = Db.Dependents.Find(id);
            return View(dependent);
        }

        //
        // POST: /Admin/Office/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            
            var dependent = Db.Dependents.Find(id);
            if (!SecurityGuard.CanEditDependent(dependent, ModelState))
            {
                return View();
            }
            Db.Dependents.Remove(dependent);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }


        public int GetSessionByEmployee(Employee emp)
        {
            var session = Db.CheckListSessions.FirstOrDefault(c => c.Employee.Id == emp.Id && c.Active);
            if (session != null)
            {
                return session.Id;
            }
            else
            {
                return 0;
            }
        }

    }
}
