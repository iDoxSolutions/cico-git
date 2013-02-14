using System;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactUser{get; set; }
        
    }
}