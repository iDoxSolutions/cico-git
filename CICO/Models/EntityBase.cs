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
        public DateTime? DateCreated { get; set; }
        [StringLength(100)]
        public string UserCreated { get; set; }
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