using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Reminder : EntityBaseWithKey
    {
        [DisplayName("Type")]
        public string Checklisttype { get; set; }
        [DisplayName("Days To Send")]
        
        public int DateToSend { get; set; }
        [DisplayName("Checklist Description")]
        [StringLength(66)]
        public string ChecklistDescription { get; set; }
        [DisplayName("Subject")]
        [StringLength(66)]
        public string  MessageSubject { get; set; }
        [DisplayName("Preface")]
        [StringLength(66)]
        public string MessagePreface { get; set; }
        [DisplayName("Closing")]
        [StringLength(66)]
        public string MessageClosing { get; set; }

        public virtual CheckListItemTemplate CheckListItemTemplate { get; set; }

        public virtual IList<SentBoxItem> SentBoxItems { get; set; }
    }
}