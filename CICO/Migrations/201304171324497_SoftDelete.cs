namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SoftDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("CheckListItemTemplates", "DateCreated", c => c.DateTime());
            AddColumn("CheckListItemTemplates", "UserCreated", c => c.String(maxLength: 100));
            AddColumn("CheckListItemTemplates", "DateEdited", c => c.DateTime());
            AddColumn("CheckListItemTemplates", "UserEdited", c => c.String(maxLength: 100));
            AddColumn("CheckListItemTemplates", "Active", c => c.Boolean(nullable: false));
            Sql("UPDATE [dbo].[CheckListItemTemplates] SET Active = 1 ");
        }
        
        public override void Down()
        {
            DropColumn("CheckListItemTemplates", "Active");
            DropColumn("CheckListItemTemplates", "UserEdited");
            DropColumn("CheckListItemTemplates", "DateEdited");
            DropColumn("CheckListItemTemplates", "UserCreated");
            DropColumn("CheckListItemTemplates", "DateCreated");
        }
    }
}
