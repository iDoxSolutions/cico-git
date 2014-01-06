namespace Cico.Models
{
    public class DependentFile:EntityBaseWithKey
    {
        public virtual SystemFile SystemFile { get; set; }
        public virtual Dependent Dependent { get; set; }
        public virtual CheckListItemSubmitionTrack CheckListItemSubmitionTrack { get; set; }
    }
}