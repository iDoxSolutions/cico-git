using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;
using Cico.Models.Helpers;

namespace Cico.Areas.Admin
{

    public class RemindersModel : EntityBaseWithKey {
        public string Checklisttype { get; set; }

        public int DateToSend { get; set; }

        public string MessageSubject { get; set; }

        public string MessagePreface { get; set; }

        public string MessageClosing { get; set; }

        public virtual CheckListItemTemplate CheckListItemTemplate { get; set; }
    }
    
    public class ReminderController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Admin/EmailSubscriptions/

        public ActionResult Index(int itemTemplate)
        {
            var reminders =
                Db.EmailSubscriptions.Include("CheckListItemTemplate")
                .Where(c => c.CheckListItemTemplate.CheckListItemTemplateId == itemTemplate)
                .Select(c=>new EmailSubscriptionModel(){Email = c.Email,Id = c.Id})
                .ToList();
            return Json(reminders);
        }

        [HttpPost]
        [HandleModelStateException]
        public ActionResult Create(Reminders model)
        {
            if (ModelState.IsValid)
            {
               // var template = Db.CheckListItemTemplates.Single(c => c.CheckListItemTemplateId == model.ItemTemplateId);
                var subs = Db.EmailSubscriptions.Add(new EmailSubscription()
                    {
                        //CheckListItemTemplate = template,
                        //Email = model.Email
                    });
                Db.SaveChanges();
                model.Id = subs.Id;
                return Json(model);
            }
            else
            {
                throw new ModelStateException(ModelState);
            }
        }

        [HttpPost]
        [HandleModelStateException]
        public ActionResult Delete(Reminders model)
        {
            var subs = Db.EmailSubscriptions.Single(c => c.Id == model.Id);
            Db.EmailSubscriptions.Remove(subs);
            Db.SaveChanges();
            return Json(model);
        }

    }
}
