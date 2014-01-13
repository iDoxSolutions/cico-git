using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class EntityBase
    {
        public EntityBase()
        {
            Active = true;
        }
        [Display(Name = "CheckList Date Created: Format mm/dd/yyyy")]
        [RegularExpression("^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$", ErrorMessage = "Invalid format for Date Created.")]
        public DateTime? DateCreated { get; set; }
        [StringLength(100)]
        public string UserCreated { get; set; }

        [Display(Name = "CheckList Date Edited: Format mm/dd/yyyy")]
        [RegularExpression("^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$", ErrorMessage = "Invalid format for Date Edited.")]
        public DateTime? DateEdited { get; set; }
        [StringLength(100)]
        public string UserEdited { get; set; }
        public bool Active { get; set; }

        public virtual void OnSave()
        {
            
        }
    }

    public class EntityBaseWithKey : EntityBase
    {
        public int Id { get; set; }
    }
}