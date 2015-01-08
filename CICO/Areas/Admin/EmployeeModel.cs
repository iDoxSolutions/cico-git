using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Cico.Models;
using Cico.Models.Authentication;

namespace Cico.Areas.Admin
{
    public class EmployeeModel : IAccessRightsModel
    {
        public EmployeeModel()
        {
            AccessRights = new List<AccessFieldRight>();
        }
        public EmployeeModel(UserSession userSession)
        {
            UserSession = userSession;
            AccessRights = new List<AccessFieldRight>();
        }

        public Employee Employee { get; set; }
        public IList<SelectListItem> Proxies { get; set; }
        public string SelectedProxy { get; set; }
        public bool EditEnabled { get; set; }
        public bool AdminEditEnabled { get; set; }
        public Staff Staff { get; set; }
        public IList<AccessFieldRight> AccessRights { get; set; }
        public UserSession UserSession { get; set; }

        public void Load(ICicoContext db)
        {
            Proxies =
                db.Staffs.Include("SystemRoles")
                    .Where(c => c.SystemRoles.Any(d => d.Name == SystemRole.UserProxy))
                    .ToList()
                    .Select(c => new SelectListItem() {Text = c.UserId, Value = c.UserId})
                    .ToList();
            if (Employee != null && Employee.Proxy != null)
            {
                SelectedProxy = Employee.Proxy.UserId;
            }
            AccessRights = db.AccessFieldRights.Include("Office").Include("AccessField").ToList();
            Staff = UserSession.GetCurrentStaff();
        }
    }
}
