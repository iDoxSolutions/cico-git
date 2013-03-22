using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Staff:EntityBase
    {
        
        [Required]
        [Key]
        public string Name { get; set; }
        public virtual IList<SystemRole> SystemRoles { get; set; }
        
    }
}