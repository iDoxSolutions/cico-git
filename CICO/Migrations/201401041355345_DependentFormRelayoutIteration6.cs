namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DependentFormRelayoutIteration6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Dependents", "SameAddressData", c => c.Boolean(nullable: false, defaultValue: false));
            
        }
        
        public override void Down()
        {
            DropColumn("Dependents", "SameAddressData");
        }
    }
}
