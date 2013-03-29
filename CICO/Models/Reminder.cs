namespace Cico.Models
{
    public class Reminder : EntityBaseWithKey
    {
        public string Checklisttype { get; set; }

        public int DateToSend { get; set; }

        public string  MessageSubject { get; set; }

        public string MessagePreface { get; set; }

        public string MessageClosing { get; set; }

        public virtual CheckListItemTemplate CheckListItemTemplate { get; set; }
    }
}