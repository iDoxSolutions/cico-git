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
    public class OfficeController : Controller
    {
        private  CicoContext db = new CicoContext();

        //
        // GET: /Admin/Office/

        public ViewResult Index()
        {
            return View(db.Offices.ToList());
        }

        //
        // GET: /Admin/Office/Details/5

        public ViewResult Details(int id)
        {
            Office Office = db.Offices.Find(id);
            return View(Office);
        }

        //
        // GET: /Admin/Office/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Office/Create

        [HttpPost]
        public ActionResult Create(Office Office)
        {
            if (ModelState.IsValid)
            {
                db.Offices.Add(Office);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(Office);
        }
        
        //
        // GET: /Admin/Office/Edit/5
 
        public ActionResult Edit(int id)
        {
            Office Office = db.Offices.Find(id);
            return View(Office);
        }

        //
        // POST: /Admin/Office/Edit/5

        [HttpPost]
        public ActionResult Edit(Office Office)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Office).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Office);
        }

        //
        // GET: /Admin/Office/Delete/5
 
        public ActionResult Delete(int id)
        {
            Office Office = db.Offices.Find(id);
            return View(Office);
        }

        //
        // POST: /Admin/Office/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Office Office = db.Offices.Find(id);
            db.Offices.Remove(Office);
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