using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int PersonID { get; set; }

        [MaxLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}

