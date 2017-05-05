namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class overrideAttributes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubLectures", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Practices", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "Artist_Id" });
            DropIndex("dbo.SubLectures", new[] { "Artist_Id" });
            DropIndex("dbo.Practices", new[] { "Artist_Id" });
            CreateTable(
                "dbo.CorrectAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false),
                        Practice_PracticeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Practices", t => t.Practice_PracticeId)
                .Index(t => t.Practice_PracticeId);
            
            AlterColumn("dbo.Courses", "CourseType", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Courses", "CourseName", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Courses", "CourseInfo", c => c.String(maxLength: 2500));
            AlterColumn("dbo.Courses", "Artist_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Lectures", "LectureName", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.SubLectures", "SubName", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.SubLectures", "LectureAdminOutput", c => c.String(nullable: false));
            AlterColumn("dbo.SubLectures", "Artist_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Practices", "PracticeName", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Practices", "TestsForUserInput", c => c.String(nullable: false));
            AlterColumn("dbo.Practices", "Artist_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Courses", "Artist_Id");
            CreateIndex("dbo.SubLectures", "Artist_Id");
            CreateIndex("dbo.Practices", "Artist_Id");
            AddForeignKey("dbo.Courses", "Artist_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SubLectures", "Artist_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Practices", "Artist_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Practices", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubLectures", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CorrectAnswers", "Practice_PracticeId", "dbo.Practices");
            DropIndex("dbo.Practices", new[] { "Artist_Id" });
            DropIndex("dbo.SubLectures", new[] { "Artist_Id" });
            DropIndex("dbo.Courses", new[] { "Artist_Id" });
            DropIndex("dbo.CorrectAnswers", new[] { "Practice_PracticeId" });
            AlterColumn("dbo.Practices", "Artist_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Practices", "TestsForUserInput", c => c.String());
            AlterColumn("dbo.Practices", "PracticeName", c => c.String());
            AlterColumn("dbo.SubLectures", "Artist_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.SubLectures", "LectureAdminOutput", c => c.String());
            AlterColumn("dbo.SubLectures", "SubName", c => c.String());
            AlterColumn("dbo.Lectures", "LectureName", c => c.String());
            AlterColumn("dbo.Courses", "Artist_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Courses", "CourseInfo", c => c.String());
            AlterColumn("dbo.Courses", "CourseName", c => c.String());
            AlterColumn("dbo.Courses", "CourseType", c => c.String());
            DropTable("dbo.CorrectAnswers");
            CreateIndex("dbo.Practices", "Artist_Id");
            CreateIndex("dbo.SubLectures", "Artist_Id");
            CreateIndex("dbo.Courses", "Artist_Id");
            AddForeignKey("dbo.Practices", "Artist_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.SubLectures", "Artist_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Courses", "Artist_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
