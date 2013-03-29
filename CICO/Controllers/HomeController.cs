using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;
using Cico.Models.Authentication;


namespace Cico.Controllers
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string FormName
        {
            get; set; }

        public string Command
        {
            get; set; }
    }

    public partial class CheckListItem
    {
        public int Id { get; set; }
        public bool Checked { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public IList<MenuItem> MenuItems { get; set; }
    }

    public partial class Staff
    {

    }

    


//public class EmployeeModel
    //{
    //   Employee employee = new Employee();
    //}


    public class HomeModel
    {
        public Employee Employee { get; set; }
        public IList<Dependent> Dependents { get; set; }

        public int? CheckListId{get; set; }

        public string CheckListName{get; set; }

        public void Load(CicoContext db)
        {
            Dependents = db.Dependents.ToList();
        }
    }


  

    public class HomeController : ControllerBase
    {
        public ActionResult Initialize()
        {
            if (UserSession.IsInitialized)
                return RedirectToAction("index");
            
            //return RedirectToAction("index");
            return View();
        }
        [HttpPost]
        public ActionResult Initialize(InitModel initModel)
        {
            if (ModelState.IsValid)
            {
                var session = UserSession.InitCheckListSession(initModel);
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
            
        }
        
        public ActionResult Index(int? id)
        {
            if (!UserSession.IsInitialized && !id.HasValue)
            {
                return RedirectToAction("initialize");
            }
            CheckListSession user = null;
            if (!id.HasValue)
            {
                user = UserSession.GetCurrent();
            }
            else
            {
                user = Db.CheckListSessions.Find(id.Value);
            }
            var model = new HomeModel()
                {
                    Employee = user.Employee,
                    CheckListId = id,
                    CheckListName = user.CheckListTemplate.Name
                };
            model.Load(Db);
            ViewBag.Message = "Please enter information";

            return View(model);
        }

        public ActionResult DeleteDependents(int id) {
            var dependent = Db.Dependents.Find(id);
            return View(dependent);
        }

        //
        // POST: /Admin/Office/Delete/5

        [HttpPost, ActionName("DeleteDependents")]
        public ActionResult DeleteDependentConfirmed(int id) {
            var dependent = Db.Dependents.Find(id);
            Db.Dependents.Remove(dependent);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            
            return View();
        }


        
    }
}



