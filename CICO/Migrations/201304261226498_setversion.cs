namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class setversion : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[Settings] SET [Value] = '1.1.5' WHERE name='AppVersion'");
        }
        
        public override void Down()
        {
        }
    }
}
