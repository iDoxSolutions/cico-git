using System;
using System.Collections.Generic;
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
        public string UserCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public string UserEdited { get; set; }
        public bool Active { get; set; }
    }

    public class EntityBaseWithKey : EntityBase
    {
        public int Id { get; set; }
    }
}