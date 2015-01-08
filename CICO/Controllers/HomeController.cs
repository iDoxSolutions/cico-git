using System;
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
using log4net;


namespace Cico.Controllers
{
    public class HomeController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CheckListsController).Name);

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
                log.DebugFormat("HomeController:Index: staff not null");
                if (!staff.ReqireCheckList && !id.HasValue)
                {
                    if (!User.IsInRole(SystemRole.UserProxy)) {
                        log.DebugFormat("HomeController:Index: not proxy, redirect to checklist index");
                        return RedirectToAction("index", "checklists", new { area = "Admin" });
                    }
                    else {
                      //kwh for panama  return RedirectToAction("proxy");
                    }

                }
            }
            

            if (!UserSession.IsInitialized(null) && !id.HasValue) {
                log.DebugFormat("HomeController:Index: redirect to initialize");
                return RedirectToAction("initialize");
            }
            CheckListSession session = null;
            bool canEditEmployee = false;
            if (!id.HasValue)
            {
                log.DebugFormat("HomeController:Index: no id value");
                session = UserSession.GetCurrent();
                // no editing until "edit my data" selected
                //canEditEmployee = true;
            }
            else
            {
                session = Db.CheckListSessions.Include("CheckListTemplate").Single(c=>c.Id==id.Value);
                if ((staff != null && UserSession.IsOfficeAdmin))
                {
                    log.DebugFormat("HomeController:Index: office admin");
                    if (staff.Office.Name == "HR" || User.IsInRole(SystemRole.GlobalAdmin))
                    {
                        log.DebugFormat("HomeController:Index: global admin");
                        // no editing until "edit my data" selected
                        //canEditEmployee = true;
                        //ViewData["canEditEmployee"] = true;
                    }
                }
                else
                {
                    if (User.IsInRole(SystemRole.GlobalAdmin))
                    {
                        // no editing until "edit my data" selected
                       // canEditEmployee = true;
                       // ViewData["canEditEmployee"] = true;
                    }
                }
            }
            //canEditEmployee = true;
            UiHelper.SetCurrentName(session.Employee.FirstName + " " + session.Employee.LastName);
            var accessRights = Db.AccessFieldRights.Include("AccessField").Include("Office").ToList();
            var model = new HomeModel()
                {
                    EmployeeModel = new EmployeeModel(UserSession) { Employee = session.Employee,
                                                          EditEnabled = canEditEmployee,
                                                          Staff = staff,
                                                          AccessRights = accessRights,
                                                         },
                    CheckListId = id,
                    CheckListName = session.CheckListTemplate.Name,
                    Tab = tab,
                    CanEditEmployee = canEditEmployee,
                    AccessRights = accessRights,
                    Staff=staff
                    
                };
            log.DebugFormat("HomeController:Index: HomeModel created");
            //if (staff.Office != null)
            //{
                //model.EmployeeModel.UserAccessRights = Db.AccessRights.Where(a => a.Office.Name == staff.Office.Name).ToList();
            //}
            model.Load(Db);
            ViewBag.Message = "Please enter information";
            log.DebugFormat("HomeController:Index: HomeModel loaded");
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



