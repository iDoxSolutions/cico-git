using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Cico.DAL;
using Cico.Models;
using Cico.ViewModels;

namespace Cico.Controllers
{
    public class HomeController : Controller
    {
       private  CICOContainer CICOContext = new CICOContainer();
        
        public ActionResult Index()
        {
            ViewBag.Message = "Please enter information";
            CheckList checklist = new CheckList();

            var item = new Item();
            item.DueDate = DateTime.Now.ToString();
            item.Name = "kens item";
            item.InstructionText = "take this a shove it";
            checklist.Items.Add(item);
            Employee employee = new Employee();

            return View(employee);
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
            var query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                + "FROM Person "
                + "WHERE EnrollmentDate IS NOT NULL "
                + "GROUP BY EnrollmentDate";
            var data = CICOContext.Database.SqlQuery<EnrollmentDateGroup>(query);
            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            CICOContext.Dispose();
            base.Dispose(disposing);
        }

    }
}
