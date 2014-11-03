using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cico.Controllers;
using Cico.Models;
using Cico.Models.Authentication;

namespace Cico.Areas.Admin
{
    //public class DependentModel
    //{
    //    public Dependent Dependent { get; set; }
    //}

    public class DependentController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Dependent/

        public ActionResult Index(int employeeId)
        {
            
            return View(Db.Dependents.Include("Employee").Where(c=>c.Employee.Id == employeeId).ToList());
        }

        public ActionResult Create(int employeeId)
        {
            return View(new DependentModel(){EmployeeId = employeeId,Dependent = new Dependent(){SameECData = true, Employee = new Employee(){Id = employeeId}}});
        }

        [HttpPost]
        public ActionResult Create(DependentModel model)
        {
            var dep = Db.Employees.Find(model.EmployeeId);
            SecurityGuard.CanEditEmployee(dep, ModelState);
            if (ModelState.IsValid)
            {
                model.Dependent.Employee = Db.Employees.Single(c => c.Id == model.EmployeeId);
                Db.Dependents.Add(model.Dependent);
                Db.SaveChanges();
                return RedirectToAction("index",new{employeeId=model.EmployeeId});
            }
            else
            {
                return View(model);
            }

        }

        public ActionResult Edit(int id)
        {
            var dependent = Db.Dependents.Single(c => c.Id == id);
            var model = new DependentModel(){Dependent = dependent,EmployeeId = dependent.Employee.Id};
            model.EditEnabled = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DependentModel model)
        {

            var dep = Db.Dependents.Find(model.Dependent.Id);
            SecurityGuard.CanEditDependent(dep, ModelState);
            if (ModelState.IsValid)
            {
                CopyValues(model.Dependent,dep);
                //model.Dependent.Employee = UserSession.GetCurrent().Employee;
               // Db.Entry(model.Dependent).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("index", new { employeeId = model.EmployeeId });
                //return RedirectToAction("index", "home",new {land="false"});
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
            var empId = dependent.Employee.Id;
            Db.Dependents.Remove(dependent);
            Db.SaveChanges();
            return RedirectToAction("Index",new {employeeId=empId});
        }


    }
}
