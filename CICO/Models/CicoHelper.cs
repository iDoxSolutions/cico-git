namespace Cico.Models
{
    public static class CicoHelper
    {
        public static string CssClass(this CheckListItemSubmitionTrack track)
        {
            if (!track.Checked)
                return "red";
            if (track.Checked && track.CheckListItemTemplate.Provisional && !track.Provisioned)
                return "yellow";
            return "green";
        }
    }
}