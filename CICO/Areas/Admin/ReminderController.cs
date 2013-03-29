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
    public class ReminderController : Cico.Controllers.ControllerBase
    {
       

        //
        // GET: /Admin/Reminder/

        public ViewResult Index()
        {
            return View(Db.Reminders.ToList());
        }

        //
        // GET: /Admin/Reminder/Details/5

        public ViewResult Details(int id)
        {
            Reminder Reminder = Db.Reminders.Find(id);
            return View(Reminder);
        }

        //
        // GET: /Admin/Reminder/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Reminder/Create

        [HttpPost]
        public ActionResult Create(Reminder Reminder)
        {
            if (ModelState.IsValid)
            {
                Db.Reminders.Add(Reminder);
                Db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(Reminder);
        }
        
        //
        // GET: /Admin/Reminder/Edit/5
 
        public ActionResult Edit(int id)
        {
            Reminder Reminder = Db.Reminders.Find(id);
            return View(Reminder);
        }

        //
        // POST: /Admin/Reminder/Edit/5

        [HttpPost]
        public ActionResult Edit(Reminder Reminder)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(Reminder).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Reminder);
        }

        //
        // GET: /Admin/Reminder/Delete/5
 
        public ActionResult Delete(int id)
        {
            Reminder Reminder = Db.Reminders.Find(id);
            return View(Reminder);
        }

        //
        // POST: /Admin/Reminder/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Reminder Reminder = Db.Reminders.Find(id);
            Db.Reminders.Remove(Reminder);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}