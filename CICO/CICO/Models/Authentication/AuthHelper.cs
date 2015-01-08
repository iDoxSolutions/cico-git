using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cico.Models.Authentication
{
    public static class AuthHelper
    {
        public  static bool FeatureEnabled(this HtmlHelper helper, string featureName)
        {
            
            var session = new UserSession(new CicoContext(), helper.ViewContext.HttpContext);
            var staff = session.GetCurrentStaff();
            if (staff == null)
                return false;
            return staff.SystemRoles.Any(c => c.AppFeatures.Any(d => d.Name == featureName));

        }
    }
}