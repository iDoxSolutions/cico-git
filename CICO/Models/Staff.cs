using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Staff:EntityBase
    {
        
        [Required]
        [Key]
        public string UserId { get; set; }
        [Required]
        public string Email { get; set; }
        public virtual IList<SystemRole> SystemRoles { get; set; }
        public virtual Office Office { get; set; }
    }
}