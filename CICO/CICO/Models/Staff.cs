using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Staff:EntityBase
    {
        
        [Required]
        [Key]
        [StringLength(100)]
        public string UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [DisplayName("Requires Check List")]
        public bool ReqireCheckList { get; set; }

        public virtual IList<SystemRole> SystemRoles { get; set; }
        public virtual Office Office { get; set; }
        public virtual IList<EmailSubscription> EmailSubscriptions { get; set; }
        public virtual IList<Employee> Proxied { get; set; } 
    }
}