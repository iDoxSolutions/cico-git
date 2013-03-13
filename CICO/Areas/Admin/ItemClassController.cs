using System;
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
    public IList<SelectListItem> FileList { get; set; }
    public int? SelectedFile { get; set; }
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
                          .ToList(),
                          CheckListItemTemplate = new CheckListItemTemplate(),
                          FileList = GetFileList()
                };
           
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
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
            var model = new ItemClassModel()
                {
                    
                    CheckListItemTemplate = template,
                    TemplateId = template.CheckListTemplate.CheckListTemplateId,
                    ItemTypes =
                        db.CheckListItemTypes.Select(c => new SelectListItem() {Text = c.Description, Value = c.Name})
                          .ToList(),
                    FileList = GetFileList()
                };
            model.SelectedFile = template.SystemFile == null ? (int?) null : template.SystemFile.Id;
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ItemClassModel model)
        {
            if (ModelState.IsValid)
            {
                
                var item =
                    db.CheckListItemTemplates.Single(
                        c => c.CheckListItemTemplateId == model.CheckListItemTemplate.CheckListItemTemplateId);


                if (model.SelectedFile.HasValue)
                {
                    var file =
                        db.SystemFiles.Single(c => c.Id == model.SelectedFile.Value);
                    db.Entry(item).Reference(c => c.SystemFile).CurrentValue = file;
                    item.SystemFile = file;
                }
                else
                {
                    db.Entry(item).State = EntityState.Modified;
                    db.Entry(item).Reference(c => c.SystemFile).CurrentValue = null;
                    item.SystemFile = null;
                }

                item.DueDays = model.CheckListItemTemplate.DueDays;
                item.Description = model.CheckListItemTemplate.Description;
                item.Item = model.CheckListItemTemplate.Item;
                item.Form = model.CheckListItemTemplate.Form;
                item.Provisional = model.CheckListItemTemplate.Provisional;
                item.NotesAccess = model.CheckListItemTemplate.NotesAccess;
                item.Dependents = model.CheckListItemTemplate.Dependents;
                item.InstructionText = model.CheckListItemTemplate.InstructionText;
                db.SaveChanges();
                return RedirectToAction("Edit", "ChecklistBuilder",new {id=model.TemplateId} );
            }
            else
            {
                model.FileList = GetFileList();
                model.ItemTypes =
                    db.CheckListItemTypes.Select(c => new SelectListItem() {Text = c.Description, Value = c.Name})
                      .ToList();
                return View(model);
            }
        }


        public IList<SelectListItem> GetFileList()
        {
            var files = Db.SystemFiles.Where(c => c.FileType == "DocTemplate").ToList();
            var ofiles = files.Select(c=>new SelectListItem(){Text = c.Description,Value = c.Id.ToString()}).ToList();
            return ofiles;
        }
       
    }
}
