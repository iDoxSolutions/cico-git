using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class DocumentTemplate : EntityBaseWithKey
    {
        [Required]
        [DisplayName("Title")]
        [StringLength(66)]
        public string DocumentTitle { get; set; }
        public virtual SystemFile SystemFile { get; set; }
        
    }
}