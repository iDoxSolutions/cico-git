using System.ComponentModel;

namespace Cico.Models.Helpers
{
    public class EmbasssyNameDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string _format;

        public EmbasssyNameDisplayNameAttribute(string format)
        {
            _format = format;
        }

        public override string DisplayName
        {
            get
            {
                return string.Format(_format,UiHelper.EmbassyNameAtt);
            }
        }
    }
}