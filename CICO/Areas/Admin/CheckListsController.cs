using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Cico.Areas.Admin
{
    public class CheckListModel
    {
        public string EmployeeName { get; set; }
    }
    public class CheckListsModel
    {
        public IList<CheckListModel> CheckListModels { get; set; }
    }

    public class CheckListsController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /CheckLists/

        public ActionResult Index()
        {
            var model = new CheckListsModel()
                {
                    
                };
            var sessions = Db.CheckListSessions.Include("Employee").ToList();
            model.CheckListModels = sessions.Select(c => new CheckListModel
                {
                    EmployeeName = c.Employee.Surname + ", " + c.Employee.GivenName
                }).ToList();
            return View(model);
        }

    }
}
