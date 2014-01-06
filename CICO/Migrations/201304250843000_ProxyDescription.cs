namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ProxyDescription : DbMigration
    {
        public override void Up()
        {
            //Eligible to be Proxy for others
            Sql("UPDATE [dbo].[SystemRoles] SET Description='Eligible to be Proxy for others' where Name='Proxy' ");
        }
        
        public override void Down()
        {
        }
    }
}
