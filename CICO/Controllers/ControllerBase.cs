using System.Web.Mvc;
using Cico.Models;
using Cico.Models.Authentication;

namespace Cico.Controllers
{
    public class ControllerBase:Controller
    {
        protected CicoContext Db { get; set; }
        protected UserSession UserSession { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Db = new CicoContext();
            UserSession = new UserSession(Db,this.HttpContext);
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Db.SaveChanges();
            base.OnActionExecuted(filterContext);
        }
    }
}