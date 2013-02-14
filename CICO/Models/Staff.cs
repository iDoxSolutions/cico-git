using System;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        [Required]
        public string Email { get; set; }
      
        
    }
}