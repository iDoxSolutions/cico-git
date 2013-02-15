using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{ 
    public class StaffController : Controller
    {
        private CicoContext db = new CicoContext();
        
        
        
        //
        // GET: /Admin/Staff/

        public ViewResult Index()
        {
            return View(db.Staffs.ToList());
        }

        //
        // GET: /Admin/Staff/Details/5

        public ViewResult Details(int id)
        {
            Staff staff = db.Staffs.Find(id);
            return View(staff);
        }

        //
        // GET: /Admin/Staff/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Staff/Create

        [HttpPost]
        public ActionResult Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(staff);
        }
        
        //
        // GET: /Admin/Staff/Edit/5
 
        public ActionResult Edit(int id)
        {
            Staff staff = db.Staffs.Find(id);
            return View(staff);
        }

        //
        // POST: /Admin/Staff/Edit/5

        [HttpPost]
        public ActionResult Edit(Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        //
        // GET: /Admin/Staff/Delete/5
 
        public ActionResult Delete(int id)
        {
            Staff staff = db.Staffs.Find(id);
            return View(staff);
        }

        //
        // POST: /Admin/Staff/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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