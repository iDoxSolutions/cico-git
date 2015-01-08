using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;
using Cico.Areas.Admin;
using Cico.Models.Helpers;
using System.Data.Entity;
using System.Data;

namespace Cico.Controllers
{
    public class EmployeeEmergencyData
    {
        public string EmergencyContactEmail { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactOfficePhone { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string EmergencyContactPhone2 { get; set; }
        public string EmergencyContactRelationship { get; set; }
        public string ResidentAddress { get; set; }
        public string HomePhone { get; set; }
    }

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
            var model = new EmployeeModel(UserSession)
            {
                Employee = employee,
                AccessRights = Db.AccessFieldRights.Include("Office").Include("AccessField").ToList(),
                Staff = UserSession.GetCurrentStaff()               
            };
            if (id == UserSession.GetCurrentUserId())
            {
                model.EditEnabled = true;
            }
            else
            {
                model.AdminEditEnabled = true;
            }
            model.Load(Db);
            return View(model);
        }

        public ActionResult AutoPopulateData(int employeeId)
        {
            var employeeData = Db.Employees.Find(employeeId);
            var dto = new EmployeeEmergencyData();
            CopyValues(employeeData,dto);
            return Json(dto);
        }

        
        [HttpPost]
        public ActionResult Edit(EmployeeModel model) {
            var employee = Db.Employees.Single(c => c.Id == model.Employee.Id);
            // new field level security makes this obsolete
            //SecurityGuard.CanEditEmployee(employee, ModelState);
            if (ModelState.IsValid)
            {

                Db.Entry(employee).State = EntityState.Modified;
                var checklist = Db.CheckListSessions.Include("CheckListTemplate").FirstOrDefault(c => c.Employee.Id == model.Employee.Id && c.Active);
                if (employee.ArrivalDate.HasValue && model.Employee.ArrivalDate.HasValue &&
                    employee.ArrivalDate.Value.Date != model.Employee.ArrivalDate.Value.Date)
                {
                    if (checklist != null)
                    {
                        if (checklist.CheckListTemplate.Type.ToUpper() == "CHECKIN")
                        {
                            checklist.ReferenceDate = model.Employee.ArrivalDate.Value;
                        }
                        
                    }
                }

                if (employee.TourEndDate.HasValue && model.Employee.TourEndDate.HasValue &&
                    employee.TourEndDate.Value.Date != model.Employee.TourEndDate.Value.Date)
                {
                    if (checklist.CheckListTemplate.Type.ToUpper() == "CHECKOUT")
                    {
                        checklist.ReferenceDate = model.Employee.TourEndDate.Value;
                    }
                }

                CopyValues(model.Employee, employee);
                Db.Entry(employee).State = EntityState.Modified;
                Db.SaveChanges();
                
                CacheHelper.RemoveKey<Employee>("user_full_name_" + UserSession.GetUserName()); 
                return RedirectToAction("index", "home",new {id=checklist.Id,land="false"});
            }
            else
            {
                model.UserSession = UserSession;
                model.EditEnabled = true;
                model.Load(Db);
                return View(model);
            }
        }

       


    }
}
