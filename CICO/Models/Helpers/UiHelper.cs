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

using Cico.Areas.Admin;
using Cico.Controllers;
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

       
        //public static MvcHtmlString CicoHTMLFor<TModel, TValue>(this HtmlHelper<TModel> html,  Expression<Func<TModel, TValue>> expression)
        //{
        //    var builder = new TagBuilder("span");
        //    builder.InnerHtml= "<div class=\"editor-label\">"+html.LabelFor(expression)+"</div>"+
        //    "<div class=\"" + /*CicoAccessFieldFor(html)  +*/ "editor-field\">" + html.EditorFor(expression) + html.ValidationMessageFor(expression)+"</div>";
        //    return new MvcHtmlString(builder.ToString());
        // }

        public static MvcHtmlString CicoEmpHTMLFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel)
        {
            var builder = new TagBuilder("span");
            var startIdx = expression.ToString().LastIndexOf(".");
            var len = expression.ToString().Length;
            string accRights = GetEmpFieldRights(empModel.EmployeeAccess, expression.ToString().Substring(startIdx + 1, len - (startIdx +1)));
           
            if (accRights == "Hide")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
                return new MvcHtmlString(builder.ToString());
            }

            if (accRights == "View")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "Edit")
            {

                if (empModel.EditEnabled)
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                    "<div class=\"editor-field\">" + html.EditorFor(expression) + html.ValidationMessageFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
                else
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                        "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
            }
           
             builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
             "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
             return new MvcHtmlString(builder.ToString());
           
        }

        public static MvcHtmlString CicoEmpHTMLFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel, string hint)
        {
            var builder = new TagBuilder("span");
            var startIdx = expression.ToString().LastIndexOf(".");
            var len = expression.ToString().Length;
            string accRights = GetEmpFieldRights(empModel.EmployeeAccess, expression.ToString().Substring(startIdx + 1, len - (startIdx + 1)));
            
            if (accRights == "Hide")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
                return new MvcHtmlString(builder.ToString());
            }

            if (accRights == "View")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "Edit")
            {
                if (empModel.EditEnabled)
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                    string.Format("<div class=\"homescreen-inline-instructions\"> {0}", hint) + "</div>" +
                    "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";

                    return new MvcHtmlString(builder.ToString());
                }
                else // a view only page is displayed change edit to view
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                        "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
            }

            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-display\">" + html.Display(" ") + "</div>";
            return new MvcHtmlString(builder.ToString());

        }

        public static MvcHtmlString CicoDepHTMLFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, DependentModel depModel)
        {
            var builder = new TagBuilder("span");
            var startIdx = expression.ToString().LastIndexOf(".");
            var len = expression.ToString().Length;
            string accRights = GetDepFieldRights(depModel.DependentAccess, expression.ToString().Substring(startIdx + 1, len - (startIdx + 1)));

            if (accRights == "View")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "Edit")
            {

                if (depModel.EditEnabled)
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                    "<div class=\"editor-field\">" + html.EditorFor(expression) + html.ValidationMessageFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
                else
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                        "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
            }
            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
            return new MvcHtmlString(builder.ToString());

        }

        public static MvcHtmlString CicoDepHTMLFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, DependentModel depModel, string hint)
        {
            var builder = new TagBuilder("span");
            var startIdx = expression.ToString().LastIndexOf(".");
            var len = expression.ToString().Length;
            string accRights = GetDepFieldRights(depModel.DependentAccess, expression.ToString().Substring(startIdx + 1, len - (startIdx + 1)));

            if (accRights == "View")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "Edit")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                string.Format("<div class=\"homescreen-inline-instructions\"> {0}", hint) + "</div>" +
                "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";

                return new MvcHtmlString(builder.ToString());
            }

            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-display\">" + html.Display(" ") + "</div>";
            return new MvcHtmlString(builder.ToString());

        }
        public static string GetEmpFieldRights(List<EmployeeAccess> accessRights, string fieldName)
        {
            if (HttpContext.Current.User.IsInRole("GlobalAdmin")) { return "Edit"; }
            if (accessRights == null) { return "Hide"; }
            var accRight = accessRights.FirstOrDefault(a => a.Name == fieldName);
            
            return "Hide";
        }

        public static string GetDepFieldRights(DependentAccess accessRights, string fieldName)
        {
            if (HttpContext.Current.User.IsInRole("GlobalAdmin")) { /*return "Edit";*/ }
            if (accessRights == null) { return "Hide"; }
            ////var accRight = accessRights.FirstOrDefault(a => a.Name == fieldName);
            //if (accRight != null)
            //{
            //    //if (accRight.Edit) return "Edit";
            //    //if (accRight.Hide) return "Hide";
            //    //if (accRight.View) return "View";
            //}
            return "Hide";
        }



        public static MvcHtmlString CicoEmpDropdownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel, string name)
        {

            var builder = new TagBuilder("span");
            var startIdx = expression.ToString().LastIndexOf(".");
            var len = expression.ToString().Length;
            string accRights = GetEmpFieldRights(empModel.EmployeeAccess, expression.ToString().Substring(startIdx + 1, len - (startIdx + 1)));
            if (accRights == "Edit")
            {
                if (empModel.EditEnabled)
                {
                    builder.InnerHtml = "<div class=\"editor-label\">" + html.LabelFor(expression) + "</div>" +
                    "<div class=\"editor-field\">" + html.DropDownListFor(expression, html.GetDropdownItems(name), "") + html.ValidationMessageFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
                else
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                         "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
            }
            if (accRights == "View")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-field\">" + html.DisplayText(" ")  + "</div>";
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString CicoEmpFieldDropdownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel , string name)
        {

            var builder = new TagBuilder("span");

            if (empModel.EditEnabled)
            {
                builder.InnerHtml = "<div class=\"editor-field\">" + html.DropDownListFor(expression, html.GetDropdownItems(name), "")  + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            else
            {
                builder.InnerHtml = "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
        }

       
        public static MvcHtmlString CicoDepDropdownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, DependentModel depModel, string name)
        {

            var builder = new TagBuilder("span");
            var startIdx = expression.ToString().LastIndexOf(".");
            var len = expression.ToString().Length;
            string accRights = GetDepFieldRights(depModel.DependentAccess, expression.ToString().Substring(startIdx + 1, len - (startIdx + 1)));
            if (accRights == "Edit")
            {
                if (depModel.EditEnabled)
                {
                    builder.InnerHtml = "<div class=\"editor-label\">" + html.LabelFor(expression) + "</div>" +
                    "<div class=\"editor-field\">" + html.DropDownListFor(expression, html.GetDropdownItems(name), "") + html.ValidationMessageFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
                else
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                         "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
            } if (accRights == "View")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-field\">" + html.DisplayText(" ") + "</div>";
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

        public static string GetUserName(HtmlHelper htmlhelper)
        {
            var httpBase = htmlhelper.ViewContext.HttpContext;
            var uname = httpBase.User.Identity.Name;
            //remove the domain - OpenText users are unique
            uname = Regex.Replace(uname, ".*\\\\(.*)", "$1", RegexOptions.None);
            return uname;
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
                    throw new ConfigurationErrorsException(string.Format("app settings EmbassyName is empty"));
                return name;
            }
            
        }

        //public static string OfficeNameAtt (int idx)
        //{
        //        CicoContext db = new CicoContext();

        //        var offices = db.Offices.OrderBy( o => o.Name);
        //        if (offices == null)
        //            throw new ConfigurationErrorsException(string.Format("Office name at position" + idx + " does not exist"));
        //        return offices.First().Name;
                     
        //}

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
        private readonly string _format;

        public EmbasssyNameDisplayNameAttribute(string format)
        {
            _format = format;
        }

        public override string DisplayName
        {
            get
            {
                return string.Format(_format,UiHelper.EmbassyNameAtt);
            }
        }
    }

    //public class OfficeNameDisplayNameAttribute : DisplayNameAttribute
    //{
    //    private readonly int _officeIdx;

    //    public OfficeNameDisplayNameAttribute(int officeIdx)
    //    {
    //        _officeIdx = officeIdx;
    //    }

    //    public override string DisplayName
    //    {
    //        get
    //        {
    //            return string.Format("{0}", UiHelper.OfficeNameAtt(_officeIdx));
    //        }
    //    }
    //}
 
}