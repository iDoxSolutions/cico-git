namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DateCompletedSession : DbMigration
    {
        public override void Up()
        {
            AddColumn("CheckListSessions", "DateCompleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("CheckListSessions", "DateCompleted");
        }
    }
}
