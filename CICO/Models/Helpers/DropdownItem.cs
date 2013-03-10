using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cico.Models.Helpers
{
    public class DropdownItem:EntityBaseWithKey
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public string ValueType { get; set; }
    }
}