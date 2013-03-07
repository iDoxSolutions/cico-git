using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class CicoContext : DbContext//, ICicoContext
    {
        public CicoContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<CheckListTemplate> CheckListTemplates { get; set; }
        public DbSet<CheckListItemType> CheckListItemTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<CheckListItemTemplate> CheckListItemTemplates { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<Setting> Settings { get; set; }
        
        public DbSet<CheckListItemSubmitionTrack> CheckListItemSubmitionTracks { get; set; }
        public DbSet<CheckListSession> CheckListSessions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            /*modelBuilder.Entity<CheckListTemplate>().HasMany(c => c.CheckListItemTemplates).WithRequired(c => c.CheckListTemplate).WillCascadeOnDelete();
            modelBuilder.Entity<CheckListItemTemplate>()
                        .HasRequired(c => c.CheckListTemplate)
                        .WithMany(c => c.CheckListItemTemplates);*/
            modelBuilder.Ignore<EntityBase>().Ignore<EntityBaseWithKey>();
           
            /*
            modelBuilder.Entity<CheckListSession>()
                .HasMany(c => c.CheckListItemSubmitionTracks)
                .WithRequired(c=>c.CheckListSession).WillCascadeOnDelete();
           
            base.OnModelCreating(modelBuilder);*/
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var added = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added).Select(e => e.Entity).OfType<EntityBase>();
            foreach (var entityBase in added)
            {
                entityBase.DateCreated = DateTime.Now;
                entityBase.DateEdited = DateTime.Now;
                if (HttpContext.Current != null)
                {
                    entityBase.UserCreated = HttpContext.Current.User.Identity.Name;
                }
            }
            return base.SaveChanges();
        }
    }
}