using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class CheckListItemType
    {
        [Key]
        public int CheckListTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }


    public class Setting
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        public string Name1 { get; set; }
    }
   


    public class CicoContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        public DbSet<CheckListTemplate> CheckListTemplates { get; set; }
        public DbSet<CheckListItemType> CheckListItemTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<CheckListItemTemplate> CheckListItemTemplates { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CheckListTemplate>().HasMany(c => c.CheckListItems);
        }
    }

    public class CicoInit :System.Data.Entity.DropCreateDatabaseIfModelChanges<Cico.Models.CicoContext>
    {
        
        protected override void Seed(CicoContext context)
        {

            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "SelfContainedForm", Description = "Self-Contained Form" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentSubmitted", Description = "Document Submitted" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentWriting", Description = "Document w/Writing" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentApproval", Description = "Document w/On-Line Approval" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "PhysicalActivity", Description = "Physical Activity" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "ProvisionalStatus", Description = "Provisional Status" });

            context.Settings.Add(new Setting(){Name = "checklisttemplate",Value = "1"});
            
            context.SaveChanges();
        } 
    }

}