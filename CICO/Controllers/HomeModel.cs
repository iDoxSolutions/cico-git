using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Cico.Models;
using Cico.Models.Services;
using Cico.Areas.Admin;
using log4net;

namespace Cico.Controllers
{
    public class HomeModel
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CheckListsController).Name);
        public bool CanEditEmployee { get; set; }
        public EmployeeModel EmployeeModel { get; set; }
        public IList<Dependent> Dependents { get; set; }
        public bool HasProxied { get; set; }
        public int? CheckListId{get; set; }

        public string CheckListName{get; set; }

        public ProxyModel ProxyModel{get; set; }

        public string Tab{ get; set; }
        public List<AccessFieldRight> AccessRights { get; set; }
        public Staff Staff { get; set; }

        public void Load(CicoContext db)
        {
            log.DebugFormat("HomeModel:Load: load dependents");
            Dependents = db.Dependents.Include("Employee").Where(c => c.Employee.Id == EmployeeModel.Employee.Id).ToList();
            log.DebugFormat("HomeModel:Load: load employee model");
            EmployeeModel.Load(db);
        }
    }
}