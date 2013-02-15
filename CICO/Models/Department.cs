using System;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Department
    {

        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Department Contact")]
        public string ContactUser{get; set; }
        
    }
}