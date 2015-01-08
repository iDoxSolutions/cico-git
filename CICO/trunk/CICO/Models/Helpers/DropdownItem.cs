using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cico.Models.Helpers
{
    public class DropdownItem:EntityBaseWithKey
    {
        //[Required]
        [StringLength(255)]
        public string Key { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        public string ValueType { get; set; }
    }
}