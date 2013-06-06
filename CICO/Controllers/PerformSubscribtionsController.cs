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

        public ActionResult Index(DateTime? referenceDate)
        {
            var refDate = referenceDate.HasValue ? referenceDate.Value : DateTime.Today;
            var service = new SubscriptionsService(Db,HttpContext);
            service.PerformDaily(refDate);
            var reminders = new RemindersService(Db, HttpContext);
            reminders.PerformDaily(refDate);

            var deactivator = new DeactivateAllEmployeesThatAreFiveDaysAfterChcekout(Db, HttpContext);
            deactivator.PerformDaily(refDate);
            return Content("OK");
        }

    }
}
