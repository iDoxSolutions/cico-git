namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fieldsrearangeiteration4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Employees", "ResidentAddress", c => c.String());
            AddColumn("Employees", "SafeWord", c => c.String(maxLength: 50));
            DropColumn("Employees", "PanamaResidentAddress");
        }
        
        public override void Down()
        {
            AddColumn("Employees", "PanamaResidentAddress", c => c.String());
            DropColumn("Employees", "SafeWord");
            DropColumn("Employees", "ResidentAddress");
        }
    }
}
