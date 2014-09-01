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
using Cico.Models.Helpers;
using Cico.Models.SharePoint;
using Cico.Models.Utils;
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
            Database.SetInitializer<CicoContext>(null);
            AreaRegistration.RegisterAllAreas();
            log.Debug("Application Started");
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            VersionSeed(new CicoContext());
            

        }

       
        protected void Application_Error(object sender, EventArgs e)
        {
            // Useful for debugging
            var ex = Server.GetLastError();
            var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
            log.Fatal(ex);
        }

        private static void VersionSeed(Cico.Models.CicoContext context)
        {
            

            string currentVersion = "1.1.31";
            var version = context.Settings.SingleOrDefault(c => c.Name == "AppVersion");
            if (version != null) {
                version.Value = currentVersion;
            }
            else {
                context.Settings.Add(new Setting() { Name = "AppVersion", Value = currentVersion });
            }
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
        private static void ConfigureLog4Net()
        {
            var hierarchy = LogManager.GetRepository() as Hierarchy;
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