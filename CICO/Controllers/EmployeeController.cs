using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Controllers
{
    public class EmployeeController : ControllerBase
    {
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var employee = UserSession.GetCurrent().Employee;
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Employee model) {
            if (ModelState.IsValid)
            {
                
                Db.Entry(model).State = EntityState.Modified;
                Db.Entry(model).Property(c => c.UserCreated).IsModified = false;
                Db.Entry(model).Property(c => c.UserId).IsModified = false;
                Db.Entry(model).Property(c => c.DateCreated).IsModified = false;
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
