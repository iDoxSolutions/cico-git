using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;


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

    public class HomeController : Controller
    {
        //private CICOEntities db = new CICOEntities();

        public ActionResult Index()
        {
            ViewBag.Message = "Please enter information";

            return View();
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

    }
}
