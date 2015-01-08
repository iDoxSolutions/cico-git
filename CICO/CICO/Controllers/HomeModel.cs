using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Cico.Models;
using Cico.Models.Services;

namespace Cico.Controllers
{
    public class HomeModel
    {
        public bool CanEditEmployee { get; set; }
        public Employee Employee { get; set; }
        public IList<Dependent> Dependents { get; set; }
        public bool HasProxied { get; set; }
        public int? CheckListId{get; set; }

        public string CheckListName{get; set; }

        public ProxyModel ProxyModel{get; set; }

        public string Tab{ get; set; }

        public void Load(CicoContext db)
        {
            Dependents = db.Dependents.Include("Employee").Where(c => c.Employee.Id == Employee.Id).ToList();
        }
    }
}