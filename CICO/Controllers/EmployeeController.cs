using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
            var employee = Db.Employees.Find(id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Employee model) {
            if (ModelState.IsValid)
            {
                var emp = Db.Employees.Single(c => c.Id == model.Id);
                Db.Entry(emp).State = EntityState.Modified;
                var checklist = Db.CheckListSessions.Include("CheckListTemplate").FirstOrDefault(c => c.Employee.Id == model.Id && c.Active);
                if (emp.ArrivalDate.HasValue && model.ArrivalDate.HasValue &&
                    emp.ArrivalDate.Value.Date != model.ArrivalDate.Value.Date)
                {
                    if (checklist != null)
                    {
                        if (checklist.CheckListTemplate.Type.ToUpper() == "CHECKIN")
                        {
                            checklist.ReferenceDate = model.ArrivalDate.Value;
                        }
                        
                    }
                }

                if (emp.TourEndDate.HasValue && model.TourEndDate.HasValue &&
                    emp.TourEndDate.Value.Date != model.TourEndDate.Value.Date)
                {
                    if (checklist.CheckListTemplate.Type.ToUpper() == "CHECKOUT")
                    {
                        checklist.ReferenceDate = model.TourEndDate.Value;
                    }
                }

                CopyValues(model,emp);
                
                Db.SaveChanges();
                
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
