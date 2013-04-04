using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class SystemRole:EntityBase
    {
        public const string GlobalAdmin = "GlobalAdmin";
        public const string OfficeAdmin = "OfficeAdmin";
        public const string UserProxy = "UserProxy";

        [Key]
        public string Name { get; set; }
        public virtual IList<Staff> Staffs { get; set; }  
    }

    
}