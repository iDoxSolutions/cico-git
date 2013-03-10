﻿using System;
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
            var items = db.DropdownItems.Where(c => c.ValueType == dropdownType)
                .Select(c=>new SelectListItem(){Text = c.Description,Value = c.Key})
                .ToList();
            return items;
        }
    }
}