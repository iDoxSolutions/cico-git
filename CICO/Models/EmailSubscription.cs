namespace Cico.Models
{
    public class EmailSubscription : EntityBaseWithKey
    {
        public string Email { get; set; }
        public virtual CheckListItemTemplate CheckListItemTemplate { get; set; }
    }
}