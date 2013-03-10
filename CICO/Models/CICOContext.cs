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
    public class CicoContext : DbContext, ICicoContext
    {
        public CicoContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public IDbSet<CheckListTemplate> CheckListTemplates { get; set; }
        public IDbSet<CheckListItemType> CheckListItemTypes { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Department> Departments { get; set; }
        public IDbSet<Staff> Staffs { get; set; }
        public IDbSet<CheckListItemTemplate> CheckListItemTemplates { get; set; }
        public IDbSet<CheckList> CheckLists { get; set; }
        public IDbSet<Setting> Settings { get; set; }
        public IDbSet<Note> Notes { get; set; }
        public IDbSet<CheckListItemSubmitionTrack> CheckListItemSubmitionTracks { get; set; }
        public IDbSet<CheckListSession> CheckListSessions { get; set; }
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