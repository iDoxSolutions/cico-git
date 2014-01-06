namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SystemRoleDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("SystemRoles", "Description", c => c.String(maxLength: 255));
            Sql("UPDATE [dbo].[SystemRoles] SET Description='May Edit Checklist' where Name='CheckListEditor' ");
            Sql("UPDATE [dbo].[SystemRoles] SET Description='Global Administration Rights' where Name='GlobalAdmin' ");
            Sql("UPDATE [dbo].[SystemRoles] SET Description='Proxy' where Name='UserProxy' ");
            Sql("UPDATE [dbo].[SystemRoles] SET Description='Office Administrator' where Name='OfficeAdmin' ");
        }
        
        public override void Down()
        {
            DropColumn("SystemRoles", "Description");
        }
    }
}
