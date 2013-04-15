using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;
using Cico.Models.Helpers;

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
                var emp = Db.Employees.Single(c => c.Id == model.Id);
                Db.Entry(emp).State = EntityState.Modified;
                CopyValues(model,emp);
                Db.SaveChanges();
                var checklist = Db.CheckListSessions.FirstOrDefault(c => c.Employee.Id == emp.Id && c.Active);
                CacheHelper.RemoveKey<Employee>("user_full_name_" + UserSession.GetUserName()); 
                return RedirectToAction("index", "home",new {id=checklist.Id,land="false"});
            }
            else
            {
                return View();
            }
        }

       


    }
}
