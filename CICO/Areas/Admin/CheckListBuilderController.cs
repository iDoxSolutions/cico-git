using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{
    public class TemplateItemModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int TemplateId { get; set; }

        public string Office
        {
            get; set; }
    }

    public class TemplateModel
    {
        public CheckListTemplate CheckListTemplate { get; set; }
        public List<SelectListItem> ItemTypes { get; set; }
        public List<TemplateItemModel> TemplateItems { get; set; }
        public List<SelectListItem> OfficeList { get; set; }

        public int? SelectedOffice
        {
            get; set; }

        public void Load(CicoContext db)
        {
            OfficeList =  db.Offices.ToList().Select(c => new SelectListItem(){Text = c.Name,Value = c.OfficeId.ToString()}).ToList();
        }
    }


    public class CheckListBuilderController : Controller
    {
        //
        // GET: /Admin/CheckListBuilder/
        private CicoContext db = new CicoContext();

        public ActionResult Index()
        {
            var list = db.CheckListTemplates.ToList();

            return View(list);
        }


        public ActionResult DeleteItem(TemplateItemModel model)
        {

            var item = db.CheckListItemTemplates.Single(c => c.CheckListItemTemplateId == model.Id);
            db.CheckListItemTemplates.Remove(item);
            db.SaveChanges();
            return Json(model);
        }

        public ActionResult AddItem(TemplateItemModel model)
        {
            var item = new CheckListItemTemplate(){Description = model.Description,Item = model.Type,Type = model.Type};
            var template = db.CheckListTemplates.Single(c => c.CheckListTemplateId == model.TemplateId);
            item.CheckListTemplate = template;
            db.CheckListItemTemplates.Add(item);
            db.SaveChanges();
            model.Id = item.CheckListItemTemplateId;
            return Json(model);
        }

        public ActionResult GetItems(int id)
        {
            var list = db.CheckListItemTemplates.Where(c=>c.CheckListTemplate.CheckListTemplateId == id)
                .ToList().Select(c=>new TemplateItemModel(){Description = c.Description,Id=c.CheckListItemTemplateId,Type = c.Item}).ToList();

            return Json(list);
        }

        public ActionResult Create()
        {
            //var cklist = new CheckListTemplate();
            //db.CheckListTemplates.Add(cklist);
            return View();
        }

        [HttpPost]
        public ActionResult Create(CheckListTemplate model)
        {
            if (ModelState.IsValid)
            {
                db.CheckListTemplates.Add(model);
                db.SaveChanges();
                return RedirectToAction("edit", new {id = model.CheckListTemplateId});
            }
            else
            {
                return View();
            }
        }



        public ActionResult Edit(int id,TemplateModel filter)
        {
            var item = db.CheckListTemplates.Single(c => c.CheckListTemplateId == id);
            var itemTemplates = filter.SelectedOffice == null ? item.CheckListItemTemplates : item.CheckListItemTemplates.Where(c=>c.Office.OfficeId == filter.SelectedOffice);
            var model = new TemplateModel()
                {
                    CheckListTemplate = item,
                    ItemTypes =
                        db.CheckListItemTypes.Select(c => new SelectListItem() {Text = c.Description, Value = c.Name})
                          .ToList(),
                          TemplateItems = itemTemplates.Select(c=>
                              new TemplateItemModel()
                                  {
                                      Description = c.Description,Id = c.CheckListItemTemplateId,TemplateId = id,Type = c.Item,Office = c.Office.Name
                                      
                                  }
                          ).ToList()

                };
            model.Load(db);

            return View(model);
        }
    }
}
