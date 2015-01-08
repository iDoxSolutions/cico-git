using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Web;
using Cico.Models.Helpers;
using System.Data.Entity;

namespace Cico.Models
{
    public class CicoContext : System.Data.Entity.DbContext, ICicoContext
    {
        public CicoContext()
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public IDbSet<AccessField> AccessFields { get; set; }
        public IDbSet<AccessFieldRight> AccessFieldRights { get; set; }
        public IDbSet<SentBoxItem> SentBoxItems { get; set; }
        public IDbSet<AppFeature> AppFeatures { get; set; }
        public IDbSet<DependentFile> DependentFiles { get; set; }
        public IDbSet<SystemRole> SystemRoles { get; set; }
        public IDbSet<EmailSubscription> EmailSubscriptions { get; set; }
        public IDbSet<SystemFile> SystemFiles { get; set; }
        public IDbSet<DropdownItem> DropdownItems { get; set; }
        public IDbSet<CheckListTemplate> CheckListTemplates { get; set; }
        public IDbSet<CheckListItemType> CheckListItemTypes { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Dependent> Dependents { get; set; }
        public IDbSet<Office> Offices { get; set; }
        public IDbSet<Staff> Staffs { get; set; }
        public IDbSet<CheckListItemTemplate> CheckListItemTemplates { get; set; }
        public IDbSet<CheckList> CheckLists { get; set; }
        public IDbSet<Setting> Settings { get; set; }
        public IDbSet<Note> Notes { get; set; }
        public IDbSet<Reminder> Reminders { get; set; }
        public IDbSet<DocumentTemplate> DocumentTemplates { get; set; }
        public IDbSet<CheckListItemSubmitionTrack> CheckListItemSubmitionTracks { get; set; }
        public IDbSet<CheckListSession> CheckListSessions { get; set; }
        public IDbSet<EmployeeAccess> EmployeeAccess { get; set; }
        public IDbSet<DependentAccess> DependentAccess { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOptional(c => c.Proxy)
                .WithMany(c => c.Proxied)
                .Map(m => m.MapKey("staffId"));

            modelBuilder.Entity<CheckListItemTemplate>()
                        .HasOptional(c => c.SystemFile)
                        .WithMany(c => c.CheckListItemTemplates);
            modelBuilder.Entity<CheckListItemTemplate>()
                        .HasOptional(c => c.Office);

            modelBuilder.Entity<CheckListItemSubmitionTrack>().Ignore(c=>c.DueDate);
            modelBuilder.Entity<CheckListItemSubmitionTrack>().Ignore(c => c.Completed);
            modelBuilder.Entity<CheckListItemSubmitionTrack>().Ignore(c => c.ForDependents);

            modelBuilder.Entity<Dependent>().HasRequired(c => c.Employee).WithMany(c=>c.Dependents);
            
            modelBuilder.Entity<CheckListItemSubmitionTrack>()
                        .HasMany(c => c.SentBoxItems)
                        .WithMany(c => c.ChecklistItems);
            modelBuilder.Entity<SystemRole>().HasMany(c => c.Staffs).WithMany(c => c.SystemRoles);
            modelBuilder.Entity<SystemRole>().HasMany(c => c.AppFeatures).WithMany(c => c.SystemRoles);
            modelBuilder.Entity<AccessFieldRight>().HasRequired(c => c.AccessField);
            modelBuilder.Entity<AccessFieldRight>().HasRequired(c => c.Office);
            modelBuilder.Ignore<EntityBase>().Ignore<EntityBaseWithKey>();
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
                entityBase.OnSave();
            }

            var modified = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified);
            if (modified != null)
            {
                foreach (var entityBase in modified)
                {
                    var ent = entityBase.Entity as EntityBase;
                    if (ent != null)
                    {
                        ent.DateEdited = DateTime.Now;

                        if (HttpContext.Current != null)
                        {
                            ent.UserEdited = HttpContext.Current.User.Identity.Name;
                        }
                        ent.OnSave();
                    }
                    
                }
            }
            try
            {
                return base.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw;
            }
            
        }
    }
}