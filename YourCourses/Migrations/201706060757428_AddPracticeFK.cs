namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPracticeFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Practices", "Sublectures_SubId", "dbo.SubLectures");
            DropIndex("dbo.Practices", new[] { "Sublectures_SubId" });
            RenameColumn(table: "dbo.Practices", name: "Sublectures_SubId", newName: "SublectureSubId");
            AlterColumn("dbo.Practices", "SublectureSubId", c => c.Int(nullable: false));
            CreateIndex("dbo.Practices", "SublectureSubId");
            AddForeignKey("dbo.Practices", "SublectureSubId", "dbo.SubLectures", "SubId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Practices", "SublectureSubId", "dbo.SubLectures");
            DropIndex("dbo.Practices", new[] { "SublectureSubId" });
            AlterColumn("dbo.Practices", "SublectureSubId", c => c.Int());
            RenameColumn(table: "dbo.Practices", name: "SublectureSubId", newName: "Sublectures_SubId");
            CreateIndex("dbo.Practices", "Sublectures_SubId");
            AddForeignKey("dbo.Practices", "Sublectures_SubId", "dbo.SubLectures", "SubId");
        }
    }
}
