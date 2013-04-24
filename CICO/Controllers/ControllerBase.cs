using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
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

        public void CopyValues<TSource, TTarget>(TSource source, TTarget target)
        {
            var sourceProperties = typeof(TSource).GetProperties().Where(p => p.CanRead && (p.PropertyType == typeof(int) 
                || p.PropertyType == typeof(string)|| p.PropertyType==typeof(DateTime)|| p.PropertyType==typeof(DateTime?)));

            foreach (var property in sourceProperties)
            {
                var targetProperty = typeof(TTarget).GetProperty(property.Name);

                if (targetProperty != null && targetProperty.CanWrite && targetProperty.PropertyType.IsAssignableFrom(property.PropertyType))
                {
                    var value = property.GetValue(source, null);

                    targetProperty.SetValue(target, value, null);
                }
            }
        }
    }
}