using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using Cico.Models;
using Cico.Models.SharePoint;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace Cico
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication).Name);
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "LandingPage", action = "index", id = UrlParameter.Optional } // Parameter defaults
                , new string[] { "Cico.Controllers" }
            );

        }

        

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            ConfigureLog4Net();
         
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CicoContext, Cico.Migrations.Configuration>());
            AreaRegistration.RegisterAllAreas();
            log.Debug("Application Started");
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            // Useful for debugging
            var ex = Server.GetLastError();
            var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
            log.Fatal(ex);
        }


        private static void ConfigureLog4Net()
        {
            Hierarchy hierarchy = LogManager.GetRepository() as Hierarchy;
            if (hierarchy != null && hierarchy.Configured)
            {
                foreach (IAppender appender in hierarchy.GetAppenders())
                {
                    if (appender is AdoNetAppender)
                    {
                        var adoNetAppender = (AdoNetAppender)appender;
                        adoNetAppender.ConnectionString = ConfigurationManager.ConnectionStrings["Cico.Models.CicoContext"].ToString();
                        adoNetAppender.ActivateOptions(); //Refresh AdoNetAppenders Settings
                    }
                }
            }
        }
    }
}