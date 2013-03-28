using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Office
    {

        public int OfficeId { get; set; }
        [DisplayName ("Office Name")]
         [StringLength(66)]
        public string Name { get; set; }

        [Required]
        [DisplayName ("Office Contact")]
        [StringLength(65)]
        public string ContactUser{get; set; }

        public virtual IList<Staff> Staffs { get; set; }
        
    }
}