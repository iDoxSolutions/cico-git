using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Cico.Areas.Admin;
using Cico.Models;
using Cico.Models.Authentication;

namespace Cico.Controllers
{
    public class DependentModel : IAccessRightsModel
    {
        public Dependent Dependent { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public DependentAccess DependentAccess { get; set; }
        public int ChecklistId{get; set; }
        public bool EditEnabled { get; set; }
        public bool AdminEditEnabled { get; set; }
        public Staff Staff { get; set; }
        public IList<AccessFieldRight> AccessRights { get; set; }
        public UserSession UserSession { get; set; }

        public void Load(ICicoContext db)
        {
            AccessRights = db.AccessFieldRights.Where(c => c.EmpDep == "Dependent").Include("AccessField").Include("Office").ToList();
            UserSession = new UserSession(db, new HttpContextWrapper(HttpContext.Current));
            Staff = UserSession.GetCurrentStaff();
        }
    }
}