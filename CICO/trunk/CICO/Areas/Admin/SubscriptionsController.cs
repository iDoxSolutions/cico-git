using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{

    
    public class SubscriptionsModel
    {
        public IList<SelectListItem> Templates { get; set; }
        public int? SelectedTemplate { get; set; }
        public void Load(ICicoContext db)
        {
            Templates = db.CheckListTemplates.ToList().Select(c => new SelectListItem(){
                Text = c.Name,
                Value = c.CheckListTemplateId.ToString(CultureInfo.InvariantCulture)}).ToList();

            if (SelectedTemplate.HasValue)
            {
                
            }
        }
    }

    public class SubscriptionsController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Admin/Subscriptions/

        public ActionResult Index(SubscriptionsModel model)
        {
            model.Load(Db);
            return View(model);
        }

    }
}
