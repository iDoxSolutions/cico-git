using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Reminder : EntityBaseWithKey
    {

        public string Checklisttype { get; set; }
        [DisplayName("Send Date")]
        [StringLength(65)]
        public int DateToSend { get; set; }
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
    }
}