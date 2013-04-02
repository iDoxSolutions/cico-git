using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class DocumentTemplate : EntityBaseWithKey
    {
        public virtual IList<CheckListItemTemplate> CheckListItemTemplates { get; set; }
        [DisplayName("Title")]
        [StringLength(66)]
        public string DocumentTitle { get; set; }
        [DisplayName("Description")]
        [StringLength(66)]
        public string Description { get; set; }
        [DisplayName("Patch")]
        [StringLength(66)]
        public string Patch { get; set; }
        [DisplayName("Extension")]
        [StringLength(4)]
        public string Extension { get; set; }
        [DisplayName("Type")]
        [StringLength(4)]
        public string FileType { get; set; }

        public override string ToString() {
            return Id.ToString();
        }
        
    }
}