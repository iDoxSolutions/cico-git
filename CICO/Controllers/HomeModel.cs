using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Cico.Models;
using Cico.Models.Services;
using Cico.Areas.Admin;

namespace Cico.Controllers
{
    public class HomeModel
    {
        public bool CanEditEmployee { get; set; }
        public EmployeeModel EmployeeModel { get; set; }
        public IList<Dependent> Dependents { get; set; }
        public bool HasProxied { get; set; }
        public int? CheckListId{get; set; }

        public string CheckListName{get; set; }

        public ProxyModel ProxyModel{get; set; }

        public string Tab{ get; set; }

        public void Load(CicoContext db)
        {
            Dependents = db.Dependents.Include("Employee").Where(c => c.Employee.Id == EmployeeModel.Employee.Id).ToList();
            EmployeeModel.Load(db);
        }
    }
}