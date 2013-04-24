namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DependentSchoolName : DbMigration
    {
        public override void Up()
        {
            AddColumn("Dependents", "SchoolName", c => c.String(maxLength: 65));
        }
        
        public override void Down()
        {
            DropColumn("Dependents", "SchoolName");
        }
    }
}
