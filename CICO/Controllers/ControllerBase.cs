using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using Cico.Models;
using Cico.Models.Authentication;

namespace Cico.Controllers
{
    public class ControllerBase:Controller
    {
        private DbTransaction _transaction;
        protected CicoContext Db { get; set; }
        protected UserSession UserSession { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Db = new CicoContext();
            ((IObjectContextAdapter)Db).ObjectContext.Connection.Open();
            _transaction = ((IObjectContextAdapter)Db).ObjectContext.Connection.BeginTransaction();
            UserSession = new UserSession(Db,this.HttpContext);
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                Db.SaveChanges();
                _transaction.Commit();
            }
            else
            {
                _transaction.Rollback();
            }
            base.OnActionExecuted(filterContext);
        }
    }
}