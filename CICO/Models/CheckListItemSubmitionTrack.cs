using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class CheckListItemSubmitionTrack:EntityBaseWithKey
    {
        public virtual CheckListSession CheckListSession { get; set; }
        public virtual CheckListItemTemplate CheckListItemTemplate { get; set; }
        public virtual ICollection<Note> Notes { get; set; } 
    }
}