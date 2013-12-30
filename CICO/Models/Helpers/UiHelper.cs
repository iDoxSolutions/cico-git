using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

      
        public static string EmbassyEmail(this HtmlHelper helper)
        {
            var name = ConfigurationManager.AppSettings["EmbassyEmail"];
            if (string.IsNullOrEmpty(name))
                throw new ConfigurationErrorsException(string.Format("app serrings EmbassyEmail is empty"));
            return name;
        }

        public static string EmbassyName(this HtmlHelper helper)
        {
            return EmbassyNameAtt;
        }

        public static void SetCurrentName(string val)
        {
            HttpContext.Current.Cache["cached_curr_user"] = val;
        }

        public static string EmbassyNameAtt
        {
            get
            {
                var name = ConfigurationManager.AppSettings["EmbassyName"];
                if (string.IsNullOrEmpty(name))
                    throw new ConfigurationErrorsException(string.Format("app serrings EmbassyName is empty"));
                return name;
            }
            
        }

        public static string SSNDisplay(this HtmlHelper helper, string ssn)
        {
            if (!string.IsNullOrEmpty(ssn)&& ssn.Length >= 4)
            {
                ssn = ssn.Substring(ssn.Length - 4);
                return string.Format("xxx-xx-{0}", ssn);
            }
            return "";
        }

    }

    public class EmbasssyNameDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string _formatName;

        public EmbasssyNameDisplayNameAttribute(string formatName)
        {
            _formatName = formatName;
        }

        public override string DisplayName
        {
            get
            {
                return string.Format(_formatName,UiHelper.EmbassyNameAtt);
            }
        }
    }
}