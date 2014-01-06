namespace Cico.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SentBox : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SentBoxItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateSent = c.DateTime(nullable: false),
                        Recipient = c.String(),
                        ReminderThreshold = c.Int(nullable: false),
                        AddressedTo = c.String(),
                        Copied = c.String(),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(maxLength: 100),
                        DateEdited = c.DateTime(),
                        UserEdited = c.String(maxLength: 100),
                        Active = c.Boolean(nullable: false),
                        Employee_Id = c.Int(),
                        Reminder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Employees", t => t.Employee_Id)
                .ForeignKey("Reminders", t => t.Reminder_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Reminder_Id);
            
            CreateTable(
                "CheckListItemSubmitionTrackSentBoxItems",
                c => new
                    {
                        CheckListItemSubmitionTrack_Id = c.Int(nullable: false),
                        SentBoxItem_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CheckListItemSubmitionTrack_Id, t.SentBoxItem_Id })
                .ForeignKey("CheckListItemSubmitionTracks", t => t.CheckListItemSubmitionTrack_Id, cascadeDelete: true)
                .ForeignKey("SentBoxItems", t => t.SentBoxItem_Id, cascadeDelete: true)
                .Index(t => t.CheckListItemSubmitionTrack_Id)
                .Index(t => t.SentBoxItem_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("CheckListItemSubmitionTrackSentBoxItems", new[] { "SentBoxItem_Id" });
            DropIndex("CheckListItemSubmitionTrackSentBoxItems", new[] { "CheckListItemSubmitionTrack_Id" });
            DropIndex("SentBoxItems", new[] { "Reminder_Id" });
            DropIndex("SentBoxItems", new[] { "Employee_Id" });
            DropForeignKey("CheckListItemSubmitionTrackSentBoxItems", "SentBoxItem_Id", "SentBoxItems");
            DropForeignKey("CheckListItemSubmitionTrackSentBoxItems", "CheckListItemSubmitionTrack_Id", "CheckListItemSubmitionTracks");
            DropForeignKey("SentBoxItems", "Reminder_Id", "Reminders");
            DropForeignKey("SentBoxItems", "Employee_Id", "Employees");
            DropTable("CheckListItemSubmitionTrackSentBoxItems");
            DropTable("SentBoxItems");
        }
    }
}
