﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;


public class ItemClassModel
{
    public CheckListItemTemplate CheckListItemTemplate { get; set; }
    public IList<SelectListItem> ItemTypes { get; set; }
    public int TemplateId { get; set; }
    public string FormType { get; set; }
    public bool IsFormSelected()
    {
        return !string.IsNullOrEmpty(FormType);
    }
}

namespace Cico.Areas.Admin
{
    public class ItemClassController : Cico.Controllers.ControllerBase
    {
        private CicoContext db = new CicoContext();

        //
        // GET: /Admin/ItemClass/

        public ActionResult Index(int templateId)
        {
            var model = new ItemClassModel()
                {
                    TemplateId = templateId,
                    ItemTypes = db.CheckListItemTypes.Select(c => new SelectListItem() { Text = c.Description, Value = c.Name })
                          .ToList()
                };
           
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ItemClassModel model)
        {
            if (ModelState.IsValid)
            {
                model.CheckListItemTemplate.CheckListTemplate = db.CheckListTemplates.Single(c => c.CheckListTemplateId==model.TemplateId);
                var template = db.CheckListItemTemplates.Add(model.CheckListItemTemplate);
                db.SaveChanges();
                return RedirectToAction("edit","checklistbuilder",new{id=model.TemplateId});
                
            }
            else
            {
                model.ItemTypes =
                    db.CheckListItemTypes.Select(c => new SelectListItem() {Text = c.Description, Value = c.Name})
                      .ToList();
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var template = Db.CheckListItemTemplates.Single(c => c.CheckListItemTemplateId == id);
            return View(new ItemClassModel()
                {
                    CheckListItemTemplate = template,
                    TemplateId = id,
                    ItemTypes = db.CheckListItemTypes.Select(c => new SelectListItem() { Text = c.Description, Value = c.Name })
                          .ToList()
                });
        }
        [HttpPost]
        public ActionResult Edit(ItemClassModel model)
        {
            if (ModelState.IsValid)
            {
                //db.CheckListItemTemplates. ( model.CheckListItemTemplate);
                db.Entry(model.CheckListItemTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "ChecklistBuilder",new {id=model.TemplateId} );
            }
            else
            {
                return View(model);
            }
        }

       
    }
}