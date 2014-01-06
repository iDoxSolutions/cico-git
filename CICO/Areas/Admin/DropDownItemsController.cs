using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models.Helpers;

namespace Cico.Areas.Admin
{
    public class DropDownItemsController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Admin/DropDownItems/

        public ActionResult Index()
        {
            return View(Db.DropdownItems.OrderBy(c=>c.ValueType).ThenBy(c=>c.Key).ToList());
        }

        public ActionResult Edit(int id)
        {
            var item = Db.DropdownItems.Find(id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(DropdownItem model)
        {
            if (ModelState.IsValid)
            {
                var item = Db.DropdownItems.Find(model.Id);
                item.Key = model.Key;
                item.Description = model.Description;
                item.ValueType = model.ValueType;
                return RedirectToAction("index");
            }
            else
            {
                return View(model);    
            }
            
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(DropdownItem model)
        {
            if (ModelState.IsValid)
            {
                Db.DropdownItems.Add(model);
                return RedirectToAction("index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            var item = Db.DropdownItems.Find(id);
            Db.DropdownItems.Remove(item);
            return RedirectToAction("index");
        }
    }
}
