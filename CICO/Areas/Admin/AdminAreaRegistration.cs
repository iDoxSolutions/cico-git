using System.Web.Mvc;

namespace Cico.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", controller = "Emploees", id = UrlParameter.Optional }, new string[] { "Cico.Areas.Admin" } 
            );
        }
    }
}
