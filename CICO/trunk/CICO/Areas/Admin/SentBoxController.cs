using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Cico.Areas.Admin
{
    public class SentBoxController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Admin/SentBox/

        public ActionResult Index(int? page)
        {
            page = page ?? 1;
            var pagdSent = Db.SentBoxItems.OrderByDescending(c => c.Id).ToPagedList(page.Value, 50);
            return View(pagdSent);
        }

        public ActionResult Show(int id)
        {
            var item = Db.SentBoxItems.Find(id);
            return View(item);
        }

    }
}
