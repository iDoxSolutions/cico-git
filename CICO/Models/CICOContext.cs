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
    }

    public class CheckListTemplate
    {
        [Key]
        public int CheckListTemplateId { get; set; }
        [Required]
        public int Name { get; set; }
    }


    public class CICOContext : DbContext
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
    }

    public class CicoInit:System.Data.Entity.DropCreateDatabaseIfModelChanges<Cico.Models.CICOContext>
    {
        
        protected override void Seed(CICOContext context)
        {
            
            context.CheckListItemTypes.Add(new CheckListItemType() {Name = "Simple Check Item"});
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "Document upload Item" });

            
            context.SaveChanges();
        } 
    }

}