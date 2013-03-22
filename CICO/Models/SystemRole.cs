using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class SystemRole:EntityBase
    {
        [Key]
        public string Name { get; set; }
        public virtual IList<Staff> Staffs { get; set; }  
    }

    
}