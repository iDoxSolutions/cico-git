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
            return Json(CheckList);
        }

        public ActionResult GetCheckList()
        {
            CICOEntities db = new CICOEntities();
            var cklist = db.CheckLists.Select(c => c.Employee.userid)
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
