using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{ 
    public class CheckListItemTemplateController : Controller
    {
        private CicoContext db = new CicoContext();

        //
        // GET: /Admin/CheckListItemTemplate/

        public ViewResult Index()
        {
            return View(db.CheckListItemTemplates.ToList());
        }

        //
        // GET: /Admin/CheckListItemTemplate/Details/5

        public ViewResult Details(int id)
        {
            CheckListItemTemplate checklistitemtemplate = db.CheckListItemTemplates.Find(id);
            return View(checklistitemtemplate);
        }

        //
        // GET: /Admin/CheckListItemTemplate/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/CheckListItemTemplate/Create

        [HttpPost]
        public ActionResult Create(CheckListItemTemplate checklistitemtemplate)
        {
            if (ModelState.IsValid)
            {
                db.CheckListItemTemplates.Add(checklistitemtemplate);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(checklistitemtemplate);
        }
        
        //
        // GET: /Admin/CheckListItemTemplate/Edit/5
 
        public ActionResult Edit(int id)
        {
            CheckListItemTemplate checklistitemtemplate = db.CheckListItemTemplates.Find(id);
            return View(checklistitemtemplate);
        }

        //
        // POST: /Admin/CheckListItemTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(CheckListItemTemplate checklistitemtemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checklistitemtemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checklistitemtemplate);
        }

        //
        // GET: /Admin/CheckListItemTemplate/Delete/5
 
        public ActionResult Delete(int id)
        {
            CheckListItemTemplate checklistitemtemplate = db.CheckListItemTemplates.Find(id);
            return View(checklistitemtemplate);
        }

        //
        // POST: /Admin/CheckListItemTemplate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CheckListItemTemplate checklistitemtemplate = db.CheckListItemTemplates.Find(id);
            db.CheckListItemTemplates.Remove(checklistitemtemplate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}