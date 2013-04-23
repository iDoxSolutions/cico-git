using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models.Subscriptions;

namespace Cico.Controllers
{
    public class PerformSubscribtionsController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /PerformSubscribtions/

        public ActionResult Index()
        {
            var service = new SubscriptionsService(Db,HttpContext);
            service.PerformDaily();
            var reminders = new RemindersService(Db, HttpContext);
            reminders.PerformDaily();
            return Content("");
        }

    }
}
