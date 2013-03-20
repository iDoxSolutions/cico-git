using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{
    public class CheckListModel
    {
        public string EmployeeName { get; set; }
        public Employee Employee { get; set; }
        public CheckListSession Session { get; set; }
        public string DateCreated{get; set; }
        public int ItemsChecked { get; set; }
    }
    public class CheckListsModel
    {
        public IList<CheckListModel> CheckListModels { get; set; }
    }

    public class CheckListsController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /CheckLists/

        public ActionResult Index()
        {
            var model = new CheckListsModel()
                {
                    
                };
            var sessions = Db.CheckListSessions.Where(c => c.Active).Include("Employee").Include("CheckListTemplate").ToList();
            model.CheckListModels = sessions.Select(c => new CheckListModel
                {
                    EmployeeName = c.Employee.Surname + ", " + c.Employee.GivenName,
                    Employee = c.Employee,
                    DateCreated = c.DateCreated.HasValue?c.DateCreated.Value.ToShortDateString():"",
                    Session = c,
                    ItemsChecked = c.CheckListItemSubmitionTracks.Count(d=>d.Checked)
                }).ToList();
            return View(model);
        }

    }
}
