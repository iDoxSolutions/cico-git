using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Text.RegularExpressions;
namespace Cico.Models.Helpers
{
    public static class UiHelper
    {
        
        public static string UserFullName(this HtmlHelper helper)
        {
            var userName = HttpContext.Current.User.Identity.Name;
            //trim off the domain - not needed because OpenNet userids are unique across domains
            userName = Regex.Replace(userName, ".*\\\\(.*)", "$1", RegexOptions.None);
            var context = new CicoContext();
            var employee =
                CacheHelper.Cache(
                    ()=>
                        { return context.Employees.FirstOrDefault(c => c.UserId == userName && c.Active); },
                    "user_full_name_" + userName);
            if (employee != null)
            {
                //SetCurrentName(employee.FirstName + " " + employee.LastName);
                return employee.FirstName + " " + employee.LastName;
            }
            else
            {
                return "";
            }
        }

        public static MvcHtmlString CicoEditorFor<TModel, TValue>(this HtmlHelper<TModel> html,  Expression<Func<TModel, TValue>> expression)
        {
            var builder = new TagBuilder("span");
            builder.InnerHtml= "<div class=\"editor-label\">"+html.LabelFor(expression)+"</div>"+
            "<div class=\"editor-field\">" + html.EditorFor(expression) + html.ValidationMessageFor(expression)+"</div>";
            return new MvcHtmlString(builder.ToString());
         }

        public static MvcHtmlString CicoEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression,string hint)
        {
            var builder = new TagBuilder("span");
            builder.InnerHtml = "<div class=\"editor-label\">" + html.LabelFor(expression) + "</div>" +
            string.Format("<div class=\"homescreen-inline-instructions\"> {0}", hint) + "</div>" +
            "<div class=\"editor-field\">" + html.EditorFor(expression) + html.ValidationMessageFor(expression) + "</div>";
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString CicoDisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var builder = new TagBuilder("span");
            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString CicoDropdownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression,string name)
        {
            var builder = new TagBuilder("span");
            builder.InnerHtml = "<div class=\"editor-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"editor-field\">" + html.DropDownListFor(expression, html.GetDropdownItems(name),"") + html.ValidationMessageFor(expression) + "</div>";
            return new MvcHtmlString(builder.ToString());
        }

        public static string GetCurrentName(this HtmlHelper helper)
        {
            if (HttpContext.Current.Cache["cached_curr_user"] == null)
            {
                return UserFullName(helper);
            }
            else
            {
               
                var currUser = HttpContext.Current.Cache["cached_curr_user"].ToString();
                SetCurrentName(UserFullName(helper));
                return currUser;

            }
        }

      
        public static string EmbassyEmail(this HtmlHelper helper)
        {
            var name = ConfigurationManager.AppSettings["EmbassyEmail"];
            if (string.IsNullOrEmpty(name))
                throw new ConfigurationErrorsException(string.Format("app settings EmbassyEmail is empty"));
            return name;
        }
        public static string EmbassyContact(this HtmlHelper helper)
        {
            var name = "PNM-ISC-STAFF@STATE.GOV";
            if (string.IsNullOrEmpty(name))
                throw new ConfigurationErrorsException(string.Format("app settings EmbassyEmail is empty"));
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

        public static MvcHtmlString SSNDisplay<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var ssn = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model as string;
            if (!string.IsNullOrEmpty(ssn)&& ssn.Length >= 4)
            {
                ssn = ssn.Substring(ssn.Length - 4);
                ssn= string.Format("xxx-xx-{0}", ssn);
            }
            //expression.
            
            var builder = new TagBuilder("span");
            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"homescreen-display\">" + ssn + "</div>";
            return new MvcHtmlString(builder.ToString());
            
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