using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cico.Controllers;
using Cico.Models;
using Cico.Models.Authentication;

namespace Cico.Areas.Admin
{
    //public class DepententModel
    //{
    //    public Dependent Dependent { get; set; }
    //}

    public class DependentController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Dependent/

        public ActionResult Index()
        {
            var employee = UserSession.GetCurrent().Employee;
            return View(employee.Dependents);
        }

        public ActionResult Create()
        {
            return View(new DepententModel(){Dependent = new Dependent(){}});
        }

        [HttpPost]
        public ActionResult Create(DepententModel model)
        {
            if (ModelState.IsValid)
            {
                model.Dependent.Employee = UserSession.GetCurrent().Employee;
                Db.Dependents.Add(model.Dependent);
                Db.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }

        }

        public ActionResult Edit(int id)
        {
            var dependent = Db.Dependents.Single(c => c.Id == id);
            var model = new DepententModel(){Dependent = dependent};
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DepententModel model) {
            if (ModelState.IsValid)
            {
                model.Dependent.Employee = UserSession.GetCurrent().Employee;
                Db.Entry(model.Dependent).State = EntityState.Modified;
                Db.SaveChanges();
                //return RedirectToAction("index");
                return RedirectToAction("index", "home",new {land="false"});
            }
            else
            {
                return View();
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
            Db.Dependents.Remove(dependent);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
