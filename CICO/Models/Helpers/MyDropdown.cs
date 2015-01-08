using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cico.Models.Helpers
{
    public static class MyDropdown
    {
        public static IList<SelectListItem> GetDropdownItems(this HtmlHelper helper, string dropdownType)
        {
            var db = new CicoContext();
            var items = db.DropdownItems.Where(c => c.ValueType == dropdownType).OrderBy(c=>c.Description)
                .Select(c=>new SelectListItem(){Text = c.Description,Value = c.Key})
                .ToList();
            return items;
        }

        public static IList<SelectListItem> GetNationsDropdownItems(this HtmlHelper helper, string dropdownType)
        {
            var db = new CicoContext();

            var items = db.DropdownItems.Where(c => c.ValueType == dropdownType).OrderBy(c => c.Description)
                .Select(c => new SelectListItem() { Text = c.Description, Value = c.Key })
                .ToList();
            var us = db.DropdownItems.Where(c => c.Description == "UNITED STATES")                                        
                .Select(c => new SelectListItem() { Text =c.Description, Value = c.Key })
                .ToList();
            return (us.Concat(items).ToList());
        }
        
    }

    public static class FieldNamesDropdown
    {
        public static System.Collections.IList GetFieldNameItems(this HtmlHelper helper, string dropdownType)
        {
            return typeof(Employee).GetProperties().Select(a => a.Name).ToList();
            
        }
    }
}