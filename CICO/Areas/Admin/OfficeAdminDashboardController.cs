using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{
    public class SubscriptionLine
    {
        private readonly Staff _staff;

        public SubscriptionLine()
        {
            
        }
        public SubscriptionLine(CheckListItemTemplate template,Staff staff)
        {
            _staff = staff;
            Load(template,staff);
        }

        public int CheckListItemTemplateId { get; set; }
        public int SubsctiptionId { get; set; }
        public bool Subscribed { get; set; }

        public string CheckListItemTemplateDescription{
            get; set; }

        public void Load(CheckListItemTemplate template,Staff staff)
        {
            this.CheckListItemTemplateId = template.CheckListItemTemplateId;
            this.CheckListItemTemplateDescription = template.Description;
            var subscription = staff.EmailSubscriptions.FirstOrDefault(
                c => c.CheckListItemTemplate.CheckListItemTemplateId == template.CheckListItemTemplateId);
            if (subscription != null)
            {
                Subscribed = true;
                SubsctiptionId = subscription.Id;
            }
        }
    }
    public class IndexDashboardOfficeModel
    {
        public IList<SelectListItem> Templates { get; set; }
        public int? SelectedTemplate { get; set; }
        public Staff Staff { get; set; }
        public IList<SubscriptionLine> SubscriptionLines { get; set; }
        public void Load(Staff staff,ICicoContext db)
        {
            Staff = staff;
            var filteredItems = db.CheckListItemTemplates as IQueryable<CheckListItemTemplate>;
            if (SelectedTemplate.HasValue)
            {
                filteredItems = filteredItems.Where(c => c.CheckListTemplate.CheckListTemplateId == SelectedTemplate);
            }
            SubscriptionLines = filteredItems.ToList().Select(c=>new SubscriptionLine(c,staff)).ToList();
            Templates = db.CheckListTemplates.ToList().Select(c => new SelectListItem(){Text = c.Name,Value = c.CheckListTemplateId.ToString()}).ToList();
        }
    }

    public class OfficeAdminDashboardController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Admin/OfficeAdminDashboard/

        public ActionResult Index( IndexDashboardOfficeModel model)
        {
            var staff = UserSession.GetCurrentStaff();

            model.Load(staff,Db);
            return View(model);
        }
        [HttpPost]
        public ActionResult RemoveSubscription(int subsId)
        {
            var subs = Db.EmailSubscriptions.Find(subsId);
            Db.EmailSubscriptions.Remove(subs);
            return Json(true);
        }

        [HttpPost]
        public ActionResult AddSubscription(int templateId)
        {
            var staff = UserSession.GetCurrentStaff();
            var subs = staff.EmailSubscriptions.FirstOrDefault(c => c.CheckListItemTemplate.CheckListItemTemplateId == templateId);
            var template = Db.CheckListItemTemplates.Find(templateId);
            if (subs == null)
            {
                
                var subsctiption = new EmailSubscription() {CheckListItemTemplate = template, Staff = staff};
                Db.EmailSubscriptions.Add(subsctiption);
                Db.SaveChanges();
                return Json(new SubscriptionLine(template, staff));
            }
            else
            {
                return Json(new SubscriptionLine(template, staff));
            }
        }
    }
}
