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
    public class EmailSubscriptionModel
    {
        private const string RegExEmailPattern =
            @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        [Required ,RegularExpression(RegExEmailPattern,ErrorMessage = "Incorrect Email")]
        public string Email { get; set; }
        public int ItemTemplateId { get; set; }
        public int Id { get; set; }
    }
    public class EmailSubscriptionsController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Admin/EmailSubscriptions/

        public ActionResult Index(int itemTemplate)
        {
            var subscriptions =
                Db.EmailSubscriptions.Include("CheckListItemTemplate")
                .Where(c => c.CheckListItemTemplate.CheckListItemTemplateId == itemTemplate)
                .Select(c=>new EmailSubscriptionModel(){Email = c.Email,Id = c.Id})
                .ToList();
            return Json(subscriptions);
        }

        [HttpPost]
        [HandleModelStateException]
        public ActionResult Create(EmailSubscriptionModel model)
        {
            if (ModelState.IsValid)
            {
                var template = Db.CheckListItemTemplates.Single(c => c.CheckListItemTemplateId == model.ItemTemplateId);
                var subs = Db.EmailSubscriptions.Add(new EmailSubscription()
                    {
                        CheckListItemTemplate = template,
                        Email = model.Email
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
        public ActionResult Delete(EmailSubscriptionModel model)
        {
            var subs = Db.EmailSubscriptions.Single(c => c.Id == model.Id);
            Db.EmailSubscriptions.Remove(subs);
            Db.SaveChanges();
            return Json(model);
        }

    }
}
