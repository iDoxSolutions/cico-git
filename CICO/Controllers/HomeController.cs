using System;
using System.Collections.Generic;
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
    }

    public partial class CheckListItem
    {
        public int Id { get; set; }
        public bool Checked { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public IList<MenuItem> MenuItems { get; set; }
    }

    public partial class Staff {
       
    }


    public class EmployeeModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }

    public class HomeModel
    {
        public EmployeeModel EmployeeModel { get; set; }
    }

    public class HomeController : Controller
    {
        //private CICOEntities db = new CICOEntities();
        UserSession _userSession = new UserSession();
        public ActionResult Index()
        {
            CicoContext db = new CicoContext();
            var cklistTypes = db.CheckListItemTypes;
            var staffmembers = db.Staffs;
            var user = _userSession.GetCurrent();
            var empModel = new EmployeeModel(){Name = "Len Hambright",Number = "10000"};
            var model = new HomeModel()
                {
                    EmployeeModel = empModel
                };

            ViewBag.Message = "Please enter information";

            return View(model);
        }

       

        public ActionResult About()
        {
            //var data = from student in db.Students
            //           group student by student.EnrollmentDate into dateGroup
            //           select new EnrollmentDateGroup()
            //           {
            //               EnrollmentDate = dateGroup.Key,
            //               StudentCount = dateGroup.Count()
            //           };
            
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
        }

        public ActionResult UpdateEmployeeData(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                return null;
            }
            else
            {
                var empModel = new EmployeeModel() { Name = "Len Hambright", Number = "10000" };
                var homemodel = new HomeModel()
                {
                    EmployeeModel = empModel
                };
                return View(homemodel);
            }
        }
    }
}



