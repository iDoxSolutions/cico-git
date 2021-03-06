﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{ 
  
    public class OfficeController : Cico.Controllers.ControllerBase
    {
       

        //
        // GET: /Admin/Office/

        public ViewResult Index()
        {
            return View(Db.Offices.ToList());
        }

        //
        // GET: /Admin/Office/Details/5

        public ViewResult Details(int id)
        {
            Office Office = Db.Offices.Find(id);
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
                Db.Offices.Add(Office);
                Db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(Office);
        }
        
        //
        // GET: /Admin/Office/Edit/5
 
        public ActionResult Edit(int id)
        {
            Office Office = Db.Offices.Find(id);
            return View(Office);
        }

        //
        // POST: /Admin/Office/Edit/5

        [HttpPost]
        public ActionResult Edit(Office Office)
        {
            if (ModelState.IsValid)
            {
                Db.Entry(Office).State = EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Office);
        }

        //
        // GET: /Admin/Office/Delete/5
 
        public ActionResult Delete(int id)
        {
            Office Office = Db.Offices.Find(id);
            return View(Office);
        }

        //
        // POST: /Admin/Office/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Office Office = Db.Offices.Find(id);
                Db.Offices.Remove(Office);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("","You cant delete office it's assigned");
                DontSave = true;
                return  View();
            }
        }
    }
}