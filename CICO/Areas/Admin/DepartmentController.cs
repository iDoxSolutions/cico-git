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
    public class DepartmentController : Controller
    {
        private  CicoContext db = new CicoContext();

        //
        // GET: /Admin/Department/

        public ViewResult Index()
        {
            return View(db.Departments.ToList());
        }

        //
        // GET: /Admin/Department/Details/5

        public ViewResult Details(int id)
        {
            Department department = db.Departments.Find(id);
            return View(department);
        }

        //
        // GET: /Admin/Department/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Department/Create

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(department);
        }
        
        //
        // GET: /Admin/Department/Edit/5
 
        public ActionResult Edit(int id)
        {
            Department department = db.Departments.Find(id);
            return View(department);
        }

        //
        // POST: /Admin/Department/Edit/5

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        //
        // GET: /Admin/Department/Delete/5
 
        public ActionResult Delete(int id)
        {
            Department department = db.Departments.Find(id);
            return View(department);
        }

        //
        // POST: /Admin/Department/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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