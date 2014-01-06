namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class fieldsrearangeiteration5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Dependents", "EmergencyContactName", c => c.String(maxLength: 65));
            AddColumn("Dependents", "PersonalMobilePhone", c => c.String(maxLength: 30));
            AddColumn("Dependents", "ResidentPhoneNumber", c => c.String(maxLength: 30));
            AddColumn("Dependents", "ResidentAddress", c => c.String(maxLength: 30));
            DropColumn("Dependents", "MiddleInitial");
            DropColumn("Dependents", "HomePhone");
            DropColumn("Dependents", "CellPhone");
            DropColumn("Dependents", "HomePhone2");
            DropColumn("Dependents", "AgencyOrSection");
            DropColumn("Dependents", "HomeAddress");
            DropColumn("Dependents", "PostOfAssignment");
            DropColumn("Dependents", "OfficePhone");
            DropColumn("Dependents", "Extension");
            DropColumn("Dependents", "EmergencyContactFirstName");
        }
        
        public override void Down()
        {
            AddColumn("Dependents", "EmergencyContactFirstName", c => c.String(maxLength: 65));
            AddColumn("Dependents", "Extension", c => c.String(maxLength: 5));
            AddColumn("Dependents", "OfficePhone", c => c.String());
            AddColumn("Dependents", "PostOfAssignment", c => c.String());
            AddColumn("Dependents", "HomeAddress", c => c.String(maxLength: 100));
            AddColumn("Dependents", "AgencyOrSection", c => c.String());
            AddColumn("Dependents", "HomePhone2", c => c.String());
            AddColumn("Dependents", "CellPhone", c => c.String());
            AddColumn("Dependents", "HomePhone", c => c.String());
            AddColumn("Dependents", "MiddleInitial", c => c.String(maxLength: 1));
            DropColumn("Dependents", "ResidentAddress");
            DropColumn("Dependents", "ResidentPhoneNumber");
            DropColumn("Dependents", "PersonalMobilePhone");
            DropColumn("Dependents", "EmergencyContactName");
        }
    }
}
