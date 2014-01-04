namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class panamafieldsrearangeiteration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Employees", "HomePhone", c => c.String());
            AddColumn("Employees", "PanamaResidentAddress", c => c.String());
            AddColumn("Employees", "PriorPost", c => c.String(maxLength: 65));
            AddColumn("Employees", "Section", c => c.String());
            DropColumn("Employees", "AgencyOrSection");
            DropColumn("Employees", "PriorPostState");
            DropColumn("Employees", "SchoolName");
        }
        
        public override void Down()
        {
            AddColumn("Employees", "SchoolName", c => c.String(maxLength: 65));
            AddColumn("Employees", "PriorPostState", c => c.String(maxLength: 65));
            AddColumn("Employees", "AgencyOrSection", c => c.String());
            DropColumn("Employees", "Section");
            DropColumn("Employees", "PriorPost");
            DropColumn("Employees", "PanamaResidentAddress");
            DropColumn("Employees", "HomePhone");
        }
    }
}
