using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Controllers
{
    public class DependentController : ControllerBase
    {
        //
        // GET: /Dependent/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            //TODO: return multiple dependents?
            //var dependent = UserSession.GetCurrent();
            //return View(dependent);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Dependent model) {
            if (ModelState.IsValid)
            {
                Db.Entry(model).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("index", "home");
            }
            else
            {
                return View();
            }
        }


    }
}
