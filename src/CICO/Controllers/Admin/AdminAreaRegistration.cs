using System.Web.Mvc;

namespace CICO.Controllers.Admin
{
    public class AdminAreaRegistration:AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin_default", 
                "Admin/{controller}/{action}/{id}", 
                new { controller="Employee", action = "index", id = "" },new string[] { 
                                "CICO.Controllers.Admin" 
                                });
        }

        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }
    }
}
