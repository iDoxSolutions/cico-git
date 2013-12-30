namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Removeemployeefields : DbMigration
    {
        public override void Up()
        {
            DropColumn("Employees", "EmployeeId");
            DropColumn("Employees", "HomePhone");
            DropColumn("Employees", "HomePhone2");
            DropColumn("Employees", "Location");
            DropColumn("Employees", "PriorPostCity");
            DropColumn("Employees", "PriorPostCountry");
            DropColumn("Employees", "PostOfAssignment");
            DropColumn("Employees", "Extension");
            DropColumn("Employees", "EmergencyContactFirstName");
            DropColumn("Employees", "BloodType");
            DropColumn("Employees", "ResidentialSafeWord");
        }
        
        public override void Down()
        {
            AddColumn("Employees", "ResidentialSafeWord", c => c.String(maxLength: 65));
            AddColumn("Employees", "BloodType", c => c.String(maxLength: 5));
            AddColumn("Employees", "EmergencyContactFirstName", c => c.String(maxLength: 65));
            AddColumn("Employees", "Extension", c => c.String());
            AddColumn("Employees", "PostOfAssignment", c => c.String());
            AddColumn("Employees", "PriorPostCountry", c => c.String(maxLength: 65));
            AddColumn("Employees", "PriorPostCity", c => c.String(maxLength: 65));
            AddColumn("Employees", "Location", c => c.String(maxLength: 20));
            AddColumn("Employees", "HomePhone2", c => c.String());
            AddColumn("Employees", "HomePhone", c => c.String());
            AddColumn("Employees", "EmployeeId", c => c.Int(nullable: false));
        }
    }
}
