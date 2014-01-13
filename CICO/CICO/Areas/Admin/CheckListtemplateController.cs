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
    public class CheckListTemplateController : Controller
    {
        private  CicoContext db = new CicoContext();

        //
        // GET: /Admin/CheckListTemplate/

        public ViewResult Index()
        {
            return View(db.CheckListTemplates.ToList());
        }

        //
        // GET: /Admin/CheckListTemplate/Details/5

        public ViewResult Details(int id)
        {
            CheckListTemplate CheckListTemplate = db.CheckListTemplates.Find(id);
            return View(CheckListTemplate);
        }

        //
        // GET: /Admin/CheckListTemplate/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/CheckListTemplate/Create

        [HttpPost]
        public ActionResult Create(CheckListTemplate CheckListTemplate)
        {
            if (ModelState.IsValid)
            {
                db.CheckListTemplates.Add(CheckListTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(CheckListTemplate);
        }
        
        //
        // GET: /Admin/CheckListTemplate/Edit/5
 
        public ActionResult Edit(int id)
        {
            CheckListTemplate CheckListTemplate = db.CheckListTemplates.Find(id);
            return View(CheckListTemplate);
        }

        //
        // POST: /Admin/CheckListTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(CheckListTemplate CheckListTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(CheckListTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CheckListTemplate);
        }

        //
        // GET: /Admin/CheckListTemplate/Delete/5
 
        public ActionResult Delete(int id)
        {
            CheckListTemplate CheckListTemplate = db.CheckListTemplates.Find(id);
            return View(CheckListTemplate);
        }

        //
        // POST: /Admin/CheckListTemplate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CheckListTemplate CheckListTemplate = db.CheckListTemplates.Find(id);
            db.CheckListTemplates.Remove(CheckListTemplate);
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