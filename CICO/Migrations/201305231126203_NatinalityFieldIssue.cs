namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class NatinalityFieldIssue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Dependents", "Nationality", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("Dependents", "Nationality", c => c.String(maxLength: 10));
        }
    }
}
