namespace Cico.Models
{
    public class Note:EntityBaseWithKey
    {
        public virtual CheckListItemSubmitionTrack CheckListItemSubmitionTrack { get; set; }
        public string Content { get; set; }
    }
}