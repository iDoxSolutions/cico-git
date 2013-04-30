namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ClearFormList : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[SystemFiles]  SET [FileType] = ''  WHERE [FileType] = 'DocTemplate'");
        }
        
        public override void Down()
        {
        }
    }
}
