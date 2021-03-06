﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;
using Cico.Models.Authentication;
using Cico.Models.Helpers;
using Cico.Areas.Admin;
using Cico.Models.Services;


namespace Cico.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Initialize(int? employeeId)
        {
            if (UserSession.IsInitialized(employeeId))
            {
                if (!employeeId.HasValue)
                {
                    employeeId = UserSession.GetCurrent().Employee.Id;
                }
                var session = Db.CheckListSessions.Single(c => c.Employee.Id == employeeId.Value && c.Active);
                return RedirectToAction("index",new{id=session.Id,land="false"});
            }

            //return RedirectToAction("index");
            return View(new InitModel(){EmpId = employeeId,ArrivalDate = DateTime.Today});
        }
        [HttpPost]
        public ActionResult Initialize(InitModel initModel)
        {
            if (ModelState.IsValid)
            {
                var session = UserSession.InitCheckListSession(initModel);
                UiHelper.SetCurrentName(session.Employee.FirstName + " " + session.Employee.LastName);
                return RedirectToAction("index",new {id=session.Id,land="false"});
            }
            else
            {
                return View();
            }
            
        }
        
        public ActionResult Index(int? id,string tab,string land)
        {

            var staff = UserSession.GetCurrentStaff();

            if (staff != null)
            {
                if (!staff.ReqireCheckList && !id.HasValue)
                {
                    if (!User.IsInRole(SystemRole.UserProxy)) {
                        return RedirectToAction("index", "checklists", new { area = "Admin" });
                    }
                    else {
                      //kwh for panama  return RedirectToAction("proxy");
                    }

                }
            }

            if (!UserSession.IsInitialized(null) && !id.HasValue) {
                return RedirectToAction("initialize");
            }
            CheckListSession session = null;
            bool canEditEmployee = false;
            if (!id.HasValue)
            {
                session = UserSession.GetCurrent();
                canEditEmployee = true;
            }
            else
            {
                session = Db.CheckListSessions.Include("CheckListTemplate").Single(c=>c.Id==id.Value);
                if ((staff != null && UserSession.IsOfficeAdmin))
                {
                    if (staff.Office.Name == "HR" || User.IsInRole(SystemRole.GlobalAdmin))
                    {
                        canEditEmployee = true;
                        ViewData["canEditEmployee"] = true;
                    }
                }
                else
                {
                    if (User.IsInRole(SystemRole.GlobalAdmin))
                    {
                        canEditEmployee = true;
                        ViewData["canEditEmployee"] = true;
                    }
                }
            }
            canEditEmployee = true;
            UiHelper.SetCurrentName(session.Employee.FirstName + " " + session.Employee.LastName);
            var model = new HomeModel()
                {
                    EmployeeModel = new EmployeeModel() { Employee = session.Employee,
                                            EditEnabled = false    
                                                         },
                    CheckListId = id,
                    CheckListName = session.CheckListTemplate.Name,
                    Tab = tab,
                    CanEditEmployee = canEditEmployee
                    
                };
            if (staff.Office != null)
            {
                //model.EmployeeModel.UserAccessRights = Db.AccessRights.Where(a => a.Office.Name == staff.Office.Name).ToList();
            }
            model.Load(Db);
            ViewBag.Message = "Please enter information";

            return View(model);
        }

        public ActionResult DeleteDependents(int id) {
            var dependent = Db.Dependents.Find(id);
            return View(dependent);
        }
        
        [HttpPost, ActionName("DeleteDependents")]
        public ActionResult DeleteDependentConfirmed(int id) {
            var dependent = Db.Dependents.Find(id);
            if (!SecurityGuard.CanEditDependent(dependent, ModelState))
            {
                return View();
            }
            var files = dependent.DependentFiles.ToList();
            foreach (var dependentFile in files)
            {
                Db.DependentFiles.Remove(dependentFile);
            }
            Db.Dependents.Remove(dependent);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SignOut()
        {
            Response.StatusCode = 401;
            Response.StatusDescription = "Unauthorized";
            Response.End();
            return Content("");
        }

        public ActionResult Proxy()
        {
            var proxyService = new ProxyService(HttpContext, Db);
            var model = proxyService.GetModel();
            if (model == null)
                return RedirectToAction("index");
            return View(model);
        }
       
    }
}



