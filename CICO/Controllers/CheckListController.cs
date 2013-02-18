using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Controllers
{
    public class CheckListController : Controller
    {

        private CicoContext db = new CicoContext();
        //
        // GET: /CheckList/
        private List<CheckListItem> CheckList
        {
            get
            {
                
                if (Session["checklist"] == null)
                {
                    var list = new List<CheckListItem>();
                    list.Add(new CheckListItem() { Checked = false, Description = "Desc1", IconClass = "iconcls", 
                        Id = 1 ,
                        MenuItems = new List<MenuItem>()
                            {
                                new MenuItem(){Description = "Menu Item",Id=1},
                                new MenuItem(){Description = "Menu Item2",Id=2}
                            }});
                    list.Add(new CheckListItem() { Checked = false, Description = "Desc2", IconClass = "iconcls", Id = 2,
                    MenuItems = new List<MenuItem>()
                            {
                                new MenuItem(){Description = "Menu Item",Id=1},
                                new MenuItem(){Description = "Menu Item2",Id=2}
                            }});
                    list.Add(new CheckListItem() { Checked = true, Description = "Desc3", IconClass = "iconcls", Id = 5 });
                    list.Add(new CheckListItem() { Checked = false, Description = "Desc4", IconClass = "iconcls", Id = 3 });
                    list.Add(new CheckListItem() { Checked = false, Description = "Desc5", IconClass = "iconcls", Id = 6 });
                    Session["checklist"] = list;
                }
                return Session["checklist"] as List<CheckListItem>;
            }

            set { Session["checklist"] = value; }
        }

       

        public ActionResult GetItems()
        {
            var list = new List<CheckListItem>();
            var s = db.Settings.FirstOrDefault(c => c.Name == "checklisttemplate");
            if (s != null)
            {
                var templateId = Int32.Parse(s.Value);
                var template = db.CheckListTemplates.Include("CheckListItems").SingleOrDefault(c => c.CheckListTemplateId == templateId);
                if (template != null)
                {
                    foreach (var checkListItemType in template.CheckListItems)
                    {
                        list.Add(new CheckListItem() { Description = checkListItemType.Description, MenuItems = GetMenuByType (checkListItemType.Type)});
                    }
                }
            }

            return Json(list);
        }

        

        private IList<MenuItem> GetMenuByType(string type)
        {
            var list = new List<MenuItem>();

            switch (type)
            {
                case "SelfContainedForm":
                    list.Add(new MenuItem(){Description = "Fill The Form"});

                    break;
                case "DocUpload":
                    list.Add(new MenuItem() { Description = "Download doc" });
                    list.Add(new MenuItem() { Description = "Upload doc" });
                    break;

            }
            
            return list;
        }

        public ActionResult UpdateItem(CheckListItem model)
        {
            var list = CheckList;
            var it = list.FirstOrDefault(c => c.Id == model.Id);
            list[list.IndexOf(it)].Checked = model.Checked;
            return Json(model);
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}
