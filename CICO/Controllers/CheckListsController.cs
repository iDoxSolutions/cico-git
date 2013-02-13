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
    public class CheckListsController : Controller
    {
        private CICOContext context = new CICOContext();

        //
        // GET: /CheckLists/

        public ViewResult Index()
        {
            return View(context.CheckLists.Include(checklist => checklist.Departments).Include(checklist => checklist.Documents).Include(checklist => checklist.CheckListItems).ToList());
        }

        //
        // GET: /CheckLists/Details/5

        public ViewResult Details(int id)
        {
            CheckList checklist = context.CheckLists.Single(x => x.Id == id);
            return View(checklist);
        }

        //
        // GET: /CheckLists/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /CheckLists/Create

        [HttpPost]
        public ActionResult Create(CheckList checklist)
        {
            if (ModelState.IsValid)
            {
                context.CheckLists.Add(checklist);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(checklist);
        }
        
        //
        // GET: /CheckLists/Edit/5
 
        public ActionResult Edit(int id)
        {
            CheckList checklist = context.CheckLists.Single(x => x.Id == id);
            return View(checklist);
        }

        //
        // POST: /CheckLists/Edit/5

        [HttpPost]
        public ActionResult Edit(CheckList checklist)
        {
            if (ModelState.IsValid)
            {
                context.Entry(checklist).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checklist);
        }

        //
        // GET: /CheckLists/Delete/5
 
        public ActionResult Delete(int id)
        {
            CheckList checklist = context.CheckLists.Single(x => x.Id == id);
            return View(checklist);
        }

        //
        // POST: /CheckLists/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckList checklist = context.CheckLists.Single(x => x.Id == id);
            context.CheckLists.Remove(checklist);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}