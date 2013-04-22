using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;
using MvcSiteMapProvider.Filters;

namespace Cico.Areas.Admin
{
    public class TemplateItemModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int TemplateId { get; set; }
        public int SubscriptionId { get; set; }
        public int Subscribed { get; set; }
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
        public bool OfficeDisabled { get; set; }
        public int? SelectedOffice
        {
            get; set; }

        public void Load(CicoContext db)
        {
            OfficeList =  db.Offices.ToList().Select(c => new SelectListItem(){Text = c.Name,Value = c.OfficeId.ToString()}).ToList();
        }
    }

    [SiteMapPreserveRouteData]
    public class CheckListBuilderController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /Admin/CheckListBuilder/
        private CicoContext db = new CicoContext();

        public ActionResult Index()
        {
            //var list = db.CheckListTemplates.ToList();
            var noPoublished = Db.CheckListTemplates.SingleOrDefault(c => c.Active && c.Published == false);

            
            var temp = Db.CheckListTemplates.OrderByDescending(c=>c.CheckListTemplateId);

            return View(temp);
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
            var item = Db.CheckListTemplates.SingleOrDefault(c=>c.CheckListTemplateId == id);
           if (User.IsInRole("OfficeAdmin"))
           {
               if(filter==null)
                   filter = new TemplateModel(){};
               filter.SelectedOffice = UserSession.GetCurrentStaff().Office.OfficeId;
               filter.OfficeDisabled=true;
           }
            
            var itemTemplates = filter.SelectedOffice == null ? item.CheckListItemTemplates : item.CheckListItemTemplates.Where(c=>c.Office.OfficeId == filter.SelectedOffice);
            itemTemplates = itemTemplates.Where(c => c.Active);
            var model = new TemplateModel()
                {
                    OfficeDisabled = filter.OfficeDisabled,
                    SelectedOffice = filter.SelectedOffice,
                    CheckListTemplate = item,
                    ItemTypes =
                        db.CheckListItemTypes.Select(c => new SelectListItem() {Text = c.Description, Value = c.Name})
                          .ToList(),
                          TemplateItems = itemTemplates.Select(c=>
                              new TemplateItemModel()
                                  {
                                      Description = c.Description,Id = c.CheckListItemTemplateId,TemplateId = id,Type = c.Item,Office = c.Office.Name,
                                      SubscriptionId = GetSubscription(c)!=null?GetSubscription(c).Id:0
                                      
                                  }
                          ).ToList()

                };
            model.Load(db);

            return View(model);
        }

        public EmailSubscription GetSubscription(CheckListItemTemplate item)
        {
            var staff = UserSession.GetCurrentStaff();
            var subs = staff.EmailSubscriptions.FirstOrDefault(
                c => c.CheckListItemTemplate.CheckListItemTemplateId == item.CheckListItemTemplateId);
            return subs;
        }

        public ActionResult Publish()
        {
            var current = Db.CheckListTemplates.SingleOrDefault(c => c.Active && c.Published == true);
            var newone  = Db.CheckListTemplates.SingleOrDefault(c => c.Active && c.Published == false);
            current.Active = false;
            newone.Published = true;
            Db.Entry(current).State = EntityState.Modified;
            Db.Entry(newone).State = EntityState.Modified;
            return RedirectToAction("index",new {id=0});
        }

        private CheckListTemplate DuplicateCurrent()
        {
            var current = Db.CheckListTemplates.Include("CheckListItemTemplates").Include("CheckListItemTemplates.Office").First(c => c.Published == true && c.Active);
            current.Published = false;
            foreach (var checkListItemTemplate in current.CheckListItemTemplates)
            {
                Db.Entry(checkListItemTemplate).State = EntityState.Added;
            }

            Db.Entry(current).State = EntityState.Added;
            db.SaveChanges();
            return current;
        }
    }
}
