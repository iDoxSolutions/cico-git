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
    public class DocumentTemplateController : Cico.Controllers.ControllerBase
    {
       

        //
        // GET: /Admin/DocumentTemplate/

        public ViewResult Index()
        {
            return View(Db.DocumentTemplates.ToList());
        }

        //
        // GET: /Admin/DocumentTemplate/Details/5

        public ViewResult Details(int id)
        {
            DocumentTemplate DocumentTemplate = Db.DocumentTemplates.Find(id);
            return View(DocumentTemplate);
        }

        //
        // GET: /Admin/DocumentTemplate/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/DocumentTemplate/Create

        [HttpPost]
        public ActionResult Create(DocumentTemplate DocumentTemplate)
        {
            if (ModelState.IsValid)
            {
                Db.DocumentTemplates.Add(DocumentTemplate);
                Db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(DocumentTemplate);
        }
        
        //
        // GET: /Admin/DocumentTemplate/Edit/5
 
        public ActionResult Edit(int id)
        {
            DocumentTemplate DocumentTemplate = Db.DocumentTemplates.Find(id);
            return View(DocumentTemplate);
        }

        //
        // POST: /Admin/DocumentTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(DocumentTemplate DocumentTemplate)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(DocumentTemplate).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(DocumentTemplate);
        }

        //
        // GET: /Admin/DocumentTemplate/Delete/5
 
        public ActionResult Delete(int id)
        {
            DocumentTemplate DocumentTemplate = Db.DocumentTemplates.Find(id);
            return View(DocumentTemplate);
        }

        //
        // POST: /Admin/DocumentTemplate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentTemplate DocumentTemplate = Db.DocumentTemplates.Find(id);
            Db.DocumentTemplates.Remove(DocumentTemplate);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}