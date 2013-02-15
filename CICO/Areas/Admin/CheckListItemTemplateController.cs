using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Controllers
{ 
    public class CheckListItemTemplateController : Controller
    {
        private CicoContext db = new CicoContext();

        //
        // GET: /CheckListItemTemplate/

        public ViewResult Index()
        {
            return View(db.CheckListTemplates.ToList());
        }

        //
        // GET: /CheckListItemTemplate/Details/5

        public ViewResult Details(int id)
        {
            CheckListTemplate checklisttemplate = db.CheckListTemplates.Find(id);
            return View(checklisttemplate);
        }

        //
        // GET: /CheckListItemTemplate/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /CheckListItemTemplate/Create

        [HttpPost]
        public ActionResult Create(CheckListTemplate checklisttemplate)
        {
            if (ModelState.IsValid)
            {
                db.CheckListTemplates.Add(checklisttemplate);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(checklisttemplate);
        }
        
        //
        // GET: /CheckListItemTemplate/Edit/5
 
        public ActionResult Edit(int id)
        {
            CheckListTemplate checklisttemplate = db.CheckListTemplates.Find(id);
            return View(checklisttemplate);
        }

        //
        // POST: /CheckListItemTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(CheckListTemplate checklisttemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checklisttemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checklisttemplate);
        }

        //
        // GET: /CheckListItemTemplate/Delete/5
 
        public ActionResult Delete(int id)
        {
            CheckListTemplate checklisttemplate = db.CheckListTemplates.Find(id);
            return View(checklisttemplate);
        }

        //
        // POST: /CheckListItemTemplate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CheckListTemplate checklisttemplate = db.CheckListTemplates.Find(id);
            db.CheckListTemplates.Remove(checklisttemplate);
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