using Cico.Models;

namespace Cico.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cico.Models.CicoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Cico.Models.CicoContext context)
        {
            string currentVersion = "1.1.17";
            var version = context.Settings.SingleOrDefault(c => c.Name == "AppVersion");
            if (version != null)
            {
                version.Value = currentVersion;
            }
            else
            {
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
    }
}
