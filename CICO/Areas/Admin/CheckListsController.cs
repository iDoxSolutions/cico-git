using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Cico.Models;
using PagedList;

namespace Cico.Areas.Admin
{
    public class CheckListModel
    {
        public string EmployeeName { get; set; }
        public Employee Employee { get; set; }
        public CheckListSession Session { get; set; }
        public string DateCreated { get { return DateValue.HasValue?DateValue.Value.ToShortDateString():""; } }
        public int ItemsChecked { get; set; }
        public int ItemsProvision { get; set; }
        public int ItemsLeft { get; set; }

        public DateTime? DateValue{get; set; }
    }
    public class CheckListsModel
    {
        public IPagedList<CheckListModel> CheckListModels { get; set; }
        public string EmployeeeName { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public int? Page { get; set; }
        [DisplayName("Check In")]
        public bool CheckIn { get; set; }
        [DisplayName("Check Out")]
        public bool CheckOut { get; set; }
    }
    public class TrackItems
    {
        
    }


    public class CheckListsController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /CheckLists/

        public ActionResult Index(CheckListsModel model)
        {
            model.Page = model.Page ?? 1;
            
            var sessions = Db.CheckListSessions.Where(c => c.Active).Include("Employee").Include("CheckListTemplate");
            if (!string.IsNullOrEmpty(model.EmployeeeName))
            {
                sessions = sessions.Where(c => c.Employee.GivenName.Contains(model.EmployeeeName) || c.Employee.Surname.Contains(model.EmployeeeName));
            }
            if (model.ReceiveDate.HasValue)
            {
                sessions = sessions.Where(c => c.ReferenceDate==model.ReceiveDate.Value);
            }
            if (model.CheckIn)
            {
                sessions = sessions.Where(c => c.CheckListTemplate.Type == "CheckIn");
            }
            if (model.CheckOut)
            {
                sessions = sessions.Where(c => c.CheckListTemplate.Type == "CheckOut");
            }

            sessions = sessions.OrderByDescending(c => c.Id);
            model.CheckListModels = sessions.Select(c => new CheckListModel
                {
                    EmployeeName = c.Employee.LastName + ", " + c.Employee.FirstName,
                    Employee = c.Employee,
                    
                    DateValue = c.DateCreated,
                    Session = c,
                    ItemsChecked = c.CheckListItemSubmitionTracks.Count(d=>d.Checked),
                    ItemsProvision = c.CheckListItemSubmitionTracks.Count(d=>d.CheckListItemTemplate.Provisional && !d.Provisioned && d.Checked),
                    ItemsLeft = c.CheckListItemSubmitionTracks.Count(d=>!d.Checked)
                }).ToPagedList(model.Page.Value, 50);
            return View(model);
        }

        public ActionResult Show(int id)
        {
            var session = Db.CheckListSessions.Include("CheckListItemSubmitionTracks").Single(c => c.Id == id);
            return View(session);
        }

        public ActionResult ApproveProvisional(int ItemId)
        {
            var item = Db.CheckListItemSubmitionTracks.Single(c => c.Id == ItemId);
            item.Provisioned = true;
            return RedirectToAction("show", new {id = item.CheckListSession.Id});
        }

        public ActionResult RejectProvisional(int ItemId)
        {
            var item = Db.CheckListItemSubmitionTracks.Single(c => c.Id == ItemId);
            item.Checked = false;
            return RedirectToAction("show", new { id = item.CheckListSession.Id });
        }

        public ActionResult Cancel(int id)
        {
            var session = Db.CheckListSessions.Find(id);
            session.Active = false;
            return RedirectToAction("index");
        }
    }
}
