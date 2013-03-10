﻿using System;
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
       
    }


    public class HomeController : ControllerBase
    {
        //private CICOEntities db = new CICOEntities();
        
        public ActionResult Index()
        {
            
            CicoContext db = new CicoContext();
            var cklistTypes = db.CheckListItemTypes;
            var staffmembers = db.Staffs;
            var user = UserSession.GetCurrent();
            var empModel = new Employee(){ GivenName = "Len Hambright", EmployeeId = 100000};
            var model = new HomeModel()
                {
                    Employee = empModel
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

        public ActionResult UpdateEmployeeData(Employee model)
        {
            if (ModelState.IsValid)
            {
                return null;
            }
            else
            {
                var empModel = new Employee() { GivenName = "Len Hambright", EmployeeId = 10000 };
                var homemodel = new HomeModel()
                {
                    Employee = empModel
                };
                return View(homemodel);
            }
        }
    }
}



