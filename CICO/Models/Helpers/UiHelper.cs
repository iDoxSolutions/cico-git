using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cico.Models.Helpers
{
    public static class UiHelper
    {
        public static string UserFullName(this HtmlHelper helper)
        {
            var userName = HttpContext.Current.User.Identity.Name;
            var context = new CicoContext();
            var employee =
                CacheHelper.Cache(
                    ()=>
                        { return context.Employees.FirstOrDefault(c => c.UserId == HttpContext.Current.User.Identity.Name && c.Active); },
                    "user_full_name_" + userName);
            if (employee != null)
            {
                return employee.FirstName + " " + employee.LastName;
            }
            else
            {
                return "";
            }
        }


        public static string GetCurrentName(this HtmlHelper helper)
        {
            if (HttpContext.Current.Cache["cached_curr_user"] == null)
            {
                return UserFullName(helper);
            }
            else
            {
                return HttpContext.Current.Cache["cached_curr_user"].ToString();
            }
        }

        public static void SetCurrentName(string val)
        {
            HttpContext.Current.Cache["cached_curr_user"] = val;

        } 
    }
}