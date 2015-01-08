using System;
using System.Collections.Generic;
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
            var dName = Regex.Replace(userName, ".*\\\\(.*)", "$1", RegexOptions.None);
            var context = new CicoContext();
            var employee = 
            CacheHelper.Cache(
                () =>
                { return context.Employees.FirstOrDefault(c => c.Active && c.UserId == userName ||  c.Active && c.UserId == dName ); },
                "user_full_name_" + userName);
            if (employee != null)
            {
                SetCurrentName(employee.FirstName + " " + employee.LastName);
                return employee.FirstName + " " + employee.LastName;
            }
            else
            {
                return dName;
            } 
        }

       
        public static MvcHtmlString CicoEmpHTMLFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel)
        {
            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(empModel, propertyName,"Employee");//GetEmpFieldRights(empModel.EmployeeAccess, expression.ToString().Substring(startIdx + 1, len - (startIdx +1)));
           
            if (accRights == "H")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
                return new MvcHtmlString(builder.ToString());
            }

            if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "E")
            {

                if (empModel.EditEnabled || empModel.AdminEditEnabled)
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

        private static string GetAccessRights(IAccessRightsModel empModel, string description, string empDep)
        {
            if (empModel.UserSession.GetCurrent().Employee.Id == empModel.Employee.Id)
            {
                if (empModel.EditEnabled)
                {
                    return "E";
                }
                return "V";
            }
            
           
            if (empModel.EditEnabled) return "E";
            if (empModel.Staff.Office == null) return "E"; //must be globaladmin
            if (empModel.Staff == null) return "H";
            var access =
                empModel.AccessRights.FirstOrDefault(
                    c => c.Office.OfficeId == empModel.Staff.Office.OfficeId && c.AccessField.FieldName == description && empDep==c.EmpDep);
            if (access == null)
            {
                return "H";
            } 
            return access.Access;
        }

        public static MvcHtmlString CicoEmpHTMLFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel, string hint)
        {
            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(empModel, propertyName,"Employee");
            
            if (accRights == "H")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
                return new MvcHtmlString(builder.ToString());
            }

            if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "E")
            {
                if (empModel.EditEnabled || empModel.AdminEditEnabled)
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                    string.Format("<div class=\"homescreen-inline-instructions\"> {0}", hint) + "</div>" +
                    "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";

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
            "<div class=\"hide-display\">" + html.Display(" ") + "</div>";
            return new MvcHtmlString(builder.ToString());

        }

        public static MvcHtmlString CicoDepHTMLFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, DependentModel depModel)
        {
            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(depModel, propertyName, "Dependent");


            if (accRights == "H")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
                return new MvcHtmlString(builder.ToString());
            }

            if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "E")
            {

                if (depModel.EditEnabled || depModel.AdminEditEnabled)
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
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(depModel, propertyName, "Dependent");

            if (accRights == "H")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
                return new MvcHtmlString(builder.ToString());
            }


            if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "E")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                string.Format("<div class=\"homescreen-inline-instructions\"> {0}", hint) + "</div>" +
                "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";

                return new MvcHtmlString(builder.ToString());
            }

            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
               "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
            return new MvcHtmlString(builder.ToString());

        }
      
        public static MvcHtmlString CicoEmpDropdownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel, string name)
        {

            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(empModel, propertyName, "Employee");
            if (accRights == "E")
            {
                if (empModel.EditEnabled || empModel.AdminEditEnabled)
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
            if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
               "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
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

        public static MvcHtmlString CicoEmpNationsDropdownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel, string name)
        {

            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(empModel, propertyName, "Employee");
            if (accRights == "E")
            {
                if (empModel.EditEnabled || empModel.AdminEditEnabled)
                {
                    builder.InnerHtml = "<div class=\"editor-label\">" + html.LabelFor(expression) + "</div>" +
                    "<div class=\"editor-field\">" + html.DropDownListFor(expression, html.GetNationsDropdownItems(name), "") + html.ValidationMessageFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
                else
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                         "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
            } if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-field\">" + html.DisplayText(" ") + "</div>";
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString CicoDepNationsDropdownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, DependentModel depModel, string name)
        {

            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(depModel, propertyName, "Employee");
            if (accRights == "E")
            {
                if (depModel.EditEnabled || depModel.AdminEditEnabled)
                {
                    builder.InnerHtml = "<div class=\"editor-label\">" + html.LabelFor(expression) + "</div>" +
                    "<div class=\"editor-field\">" + html.DropDownListFor(expression, html.GetNationsDropdownItems(name), "") + html.ValidationMessageFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
                else
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                         "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                    return new MvcHtmlString(builder.ToString());
                }
            } if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }
            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-field\">" + html.DisplayText(" ") + "</div>";
            return new MvcHtmlString(builder.ToString());
        }
       
        public static MvcHtmlString CicoDepDropdownFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, DependentModel depModel, string name)
        {

            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(depModel, propertyName, "Dependent");
            if (accRights == "E")
            {
                if (depModel.EditEnabled || depModel.AdminEditEnabled)
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
            } if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                                     "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                return new MvcHtmlString(builder.ToString());
            }

            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
               "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
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

        public static string GetDepEmployeeName(this HtmlHelper helper, ICollection<Cico.Models.Dependent> dependents)
        {
            var dep = dependents.First();
            return dep.Employee.FirstName + " " + dep.Employee.LastName;
        }

        public static string GetDependentName(this HtmlHelper helper, Cico.Controllers.DependentModel depModel)
        {
            
            return depModel.Dependent.FirstName + " " + depModel.Dependent.LastName;
        }
        public static string GetDepModelEmployeeName(this HtmlHelper helper, Cico.Controllers.DependentModel depModel)
        {
            return depModel.Dependent.Employee.FirstName + " " + depModel.Dependent.Employee.LastName;
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


        public static MvcHtmlString SSNDisplay<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel)
        {
            var ssn = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model as string;
            if (!string.IsNullOrEmpty(ssn)&& ssn.Length >= 4)
            {
                ssn = ssn.Substring(ssn.Length - 4);
                ssn= string.Format("xxx-xx-{0}", ssn);
            }
            //expression.
            
            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(empModel, propertyName, "Employee");
            if (accRights == "H")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
                return new MvcHtmlString(builder.ToString());
            }

            if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>";
                if (ssn == null)
                {
                    builder.InnerHtml = builder.InnerHtml +
                                   "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                }
                else
                {
                    builder.InnerHtml = builder.InnerHtml +
                                   "<div class=\"homescreen-display\">" + ssn + "</div>";
                } 
                                     
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "E")
            {

                if (empModel.EditEnabled || empModel.AdminEditEnabled)
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>";
                    if (ssn == null)
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";
                    }
                    else
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";
                    }
                   
                    return new MvcHtmlString(builder.ToString());
                }
                else
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>";
                    if (ssn == null)
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                    }
                    else
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"homescreen-display\">" + ssn + "</div>";
                    } 

                    return new MvcHtmlString(builder.ToString());
                }
            }

            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
            return new MvcHtmlString(builder.ToString());
            
        }

        public static MvcHtmlString PassportDisplay<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, EmployeeModel empModel)
        {
            var passport = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model as string;
            if (!string.IsNullOrEmpty(passport) && passport.Length >= 4)
            {
                passport = passport.Substring(passport.Length - 4);
                passport = string.Format("xxxxx{0}", passport);
            }
            
            //expression.

            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(empModel, propertyName, "Employee");
            if (accRights == "H")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
                return new MvcHtmlString(builder.ToString());
            }

            if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>";
                if (passport == null)
                {
                    builder.InnerHtml = builder.InnerHtml +
                                   "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                }
                else
                {
                    builder.InnerHtml = builder.InnerHtml +
                                   "<div class=\"homescreen-display\">" + passport + "</div>";
                } 
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "E")
            {

                if (empModel.EditEnabled || empModel.AdminEditEnabled)
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>";
                    if (passport == null)
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";
                    }
                    else
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";
                    }
                    return new MvcHtmlString(builder.ToString());
                }
                else
                {
                    builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>";
                    if (passport == null)
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"homescreen-display\">" + html.EditorFor(expression) + "</div>";
                    }
                    else
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"homescreen-display\">" + passport + "</div>";
                    }

                    return new MvcHtmlString(builder.ToString());
                }
            }

            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString DepPassportDisplay<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, DependentModel depModel)
        {
            var passport = ModelMetadata.FromLambdaExpression(expression, html.ViewData).Model as string;
            if (!string.IsNullOrEmpty(passport) && passport.Length >= 4)
            {
                passport = passport.Substring(passport.Length - 4);
                passport = string.Format("xxxxx{0}", passport);
            }

            //expression.

            var builder = new TagBuilder("span");
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var propertyName = metadata.PropertyName;
            string accRights = GetAccessRights(depModel, propertyName, "Dependent");
            if (accRights == "H")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
                "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
                return new MvcHtmlString(builder.ToString());
            }

            if (accRights == "V")
            {
                builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>";
                if (passport == null)
                {
                    builder.InnerHtml = builder.InnerHtml +
                                   "<div class=\"homescreen-display\">" + html.DisplayFor(expression) + "</div>";
                }
                else
                {
                    builder.InnerHtml = builder.InnerHtml +
                                   "<div class=\"homescreen-display\">" + passport + "</div>";
                }
                return new MvcHtmlString(builder.ToString());
            }
            if (accRights == "E")
            {

                if (depModel.EditEnabled || depModel.AdminEditEnabled)
                {
                    builder.InnerHtml =  "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>";
                    if (passport == null)
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";
                    }
                    else
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"editor-field\">" + html.EditorFor(expression) + "</div>";
                    }
                    return new MvcHtmlString(builder.ToString());
                }
                else
                {
                    builder.InnerHtml =  "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>";
                    if (passport == null)
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"homescreen-display\">" + html.EditorFor(expression) + "</div>";
                    }
                    else
                    {
                        builder.InnerHtml = builder.InnerHtml +
                                       "<div class=\"homescreen-display\">" + html.EditorFor(expression) + "</div>";
                    }

                    return new MvcHtmlString(builder.ToString());
                }
            }

            builder.InnerHtml = "<div class=\"homescreen-label\">" + html.LabelFor(expression) + "</div>" +
            "<div class=\"hide-display\">" + html.Display("  ") + "</div>";
            return new MvcHtmlString(builder.ToString());
        }
    }

    
}