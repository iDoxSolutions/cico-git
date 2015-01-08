using System.Collections.Generic;
using Cico.Models;
using Cico.Models.Authentication;

namespace Cico.Areas.Admin
{
    public  interface IAccessRightsModel
    {
        Staff Staff { get; set; }
        Employee Employee { get; set; }
        IList<AccessFieldRight> AccessRights { get; set; }
        bool EditEnabled { get; set; }
        UserSession UserSession { get; set; }
    }
}