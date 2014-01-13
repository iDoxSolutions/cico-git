using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class SentBoxItem:EntityBaseWithKey
    {
        public DateTime DateSent { get; set; }
        public string Recipient { get; set; }
        public int ReminderThreshold { get; set; }
        public string AddressedTo { get; set; }
        public string Copied { get; set; }
        public virtual IList<CheckListItemSubmitionTrack> ChecklistItems { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Reminder Reminder { get; set; }
       
    }
}