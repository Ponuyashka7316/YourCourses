namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMark : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PracticeAndUserMarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mark = c.Int(nullable: false),
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        PracticePracticeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId, cascadeDelete: false)
                .ForeignKey("dbo.Practices", t => t.PracticePracticeId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.PracticePracticeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PracticeAndUserMarks", "PracticePracticeId", "dbo.Practices");
            DropForeignKey("dbo.PracticeAndUserMarks", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.PracticeAndUserMarks", new[] { "PracticePracticeId" });
            DropIndex("dbo.PracticeAndUserMarks", new[] { "ArtistId" });
            DropTable("dbo.PracticeAndUserMarks");
        }
    }
}
