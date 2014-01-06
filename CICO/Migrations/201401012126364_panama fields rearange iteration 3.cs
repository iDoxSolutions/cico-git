namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class panamafieldsrearangeiteration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Employees", "EmergencyContactName", c => c.String(maxLength: 65));
            DropColumn("Employees", "EmergencyContactLastName");
        }
        
        public override void Down()
        {
            AddColumn("Employees", "EmergencyContactLastName", c => c.String(maxLength: 65));
            DropColumn("Employees", "EmergencyContactName");
        }
    }
}
