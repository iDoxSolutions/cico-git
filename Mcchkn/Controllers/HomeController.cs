using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mcchkn.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Mexico Check-In Check-Out Application";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Employee data";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CheckList()
        {
            return View();
        }

        public ActionResult MainPage() {
            return View();
        }
    }
}
