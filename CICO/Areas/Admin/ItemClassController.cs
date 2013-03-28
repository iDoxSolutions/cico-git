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
    public int SelectedOffice { get; set; }
    public IList<SelectListItem> OfficeList { get; set; }
    public bool IsFormSelected()
    {
        return !string.IsNullOrEmpty(FormType);
    }

    public bool Editable { get { return true; } }

    public static ItemClassModel Load(CicoContext db,ItemClassModel model)
    {
        model = model?? new ItemClassModel();
        model.SelectedOffice = model.CheckListItemTemplate !=null && model.CheckListItemTemplate.Office != null ? model.CheckListItemTemplate.Office.OfficeId :0;
        model.ItemTypes = db.CheckListItemTypes.Select(c => new SelectListItem() {Text = c.Description, Value = c.Name})
                            .ToList();
        model.OfficeList = db.Offices.ToList().Select(c=>new SelectListItem(){Text = c.Name,Value = c.OfficeId.ToString()}).ToList();
        var files = db.SystemFiles.Where(c => c.FileType == "DocTemplate").ToList();
        model.FileList = files.Select(c => new SelectListItem() { Text = c.Description, Value = c.Id.ToString() }).ToList();
        return model;
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
            var model = ItemClassModel.Load(db,null);
            model.TemplateId = templateId;
            var template = Db.CheckListTemplates.Single(c => c.CheckListTemplateId == model.TemplateId);
           // model.Editable = !template.Published;
            model.CheckListItemTemplate = new CheckListItemTemplate();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(ItemClassModel model)
        {
            if (ModelState.IsValid)
            {
                model.CheckListItemTemplate.CheckListTemplate = db.CheckListTemplates.Single(c => c.CheckListTemplateId==model.TemplateId);
                if (model.SelectedFile.HasValue)
                {
                    var file =
                        db.SystemFiles.Single(c => c.Id == model.SelectedFile.Value);
                    model.CheckListItemTemplate.SystemFile = file;
                }

                model.CheckListItemTemplate.Office = db.Offices.Single(c => c.OfficeId == model.SelectedOffice);
                var template = db.CheckListItemTemplates.Add(model.CheckListItemTemplate);
                db.SaveChanges();
                if (UserSession.IsOfficeAdmin)
                {
                    return RedirectToAction("index", "OfficeAdminDashboard");
                }
                return RedirectToAction("edit","checklistbuilder",new{id=model.TemplateId});
                
            }
            else
            {
                ItemClassModel.Load(db, model);
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
                    //Editable = !template.CheckListTemplate.Published && template.CheckListTemplate.Active
                 
                };
            ItemClassModel.Load(db, model);
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
                item.Office = db.Offices.Single(c => c.OfficeId == model.SelectedOffice);
                item.DueDays = model.CheckListItemTemplate.DueDays;
                item.Description = model.CheckListItemTemplate.Description;
                item.Item = model.CheckListItemTemplate.Item;
                item.Form = model.CheckListItemTemplate.Form;
                item.Provisional = model.CheckListItemTemplate.Provisional;
                item.NotesAccess = model.CheckListItemTemplate.NotesAccess;
                item.Dependents = model.CheckListItemTemplate.Dependents;
                item.InstructionText = model.CheckListItemTemplate.InstructionText;
                item.ApprovalText = model.CheckListItemTemplate.ApprovalText;
                item.CompleteCheckList = model.CheckListItemTemplate.CompleteCheckList;
                db.SaveChanges();
                if (UserSession.IsOfficeAdmin)
                {
                    return RedirectToAction("index", "OfficeAdminDashboard");
                }
                return RedirectToAction("Edit", "ChecklistBuilder",new {id=model.TemplateId} );
            }
            else
            {
                ItemClassModel.Load(db, model);
                var template = Db.CheckListTemplates.Single(c => c.CheckListTemplateId == model.TemplateId);
               // model.Editable = !template.Published && template.Active;
                return View(model);
            }
        }


       
    }
}
