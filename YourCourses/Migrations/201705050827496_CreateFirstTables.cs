namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFirstTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseType = c.String(),
                        CourseName = c.String(),
                        CourseInfo = c.String(),
                        DateOfCourseCreation = c.DateTime(nullable: false),
                        Artist_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id)
                .Index(t => t.Artist_Id);
            
            CreateTable(
                "dbo.Lectures",
                c => new
                    {
                        LectureId = c.Int(nullable: false, identity: true),
                        LectureName = c.String(),
                        Course_CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.LectureId)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.SubLectures",
                c => new
                    {
                        SubId = c.Int(nullable: false, identity: true),
                        SubName = c.String(),
                        SubImageType = c.Byte(nullable: false),
                        LectureAdminOutput = c.String(),
                        Artist_Id = c.String(maxLength: 128),
                        Lecture_LectureId = c.Int(),
                    })
                .PrimaryKey(t => t.SubId)
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id)
                .ForeignKey("dbo.Lectures", t => t.Lecture_LectureId)
                .Index(t => t.Artist_Id)
                .Index(t => t.Lecture_LectureId);
            
            CreateTable(
                "dbo.Practices",
                c => new
                    {
                        PracticeId = c.Int(nullable: false, identity: true),
                        PracticeName = c.String(),
                        PracticeUserInput = c.String(),
                        TestsForUserInput = c.String(),
                        Artist_Id = c.String(maxLength: 128),
                        SubLecture_SubId = c.Int(),
                    })
                .PrimaryKey(t => t.PracticeId)
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id)
                .ForeignKey("dbo.SubLectures", t => t.SubLecture_SubId)
                .Index(t => t.Artist_Id)
                .Index(t => t.SubLecture_SubId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.SubLectures", "Lecture_LectureId", "dbo.Lectures");
            DropForeignKey("dbo.Practices", "SubLecture_SubId", "dbo.SubLectures");
            DropForeignKey("dbo.Practices", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubLectures", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Practices", new[] { "SubLecture_SubId" });
            DropIndex("dbo.Practices", new[] { "Artist_Id" });
            DropIndex("dbo.SubLectures", new[] { "Lecture_LectureId" });
            DropIndex("dbo.SubLectures", new[] { "Artist_Id" });
            DropIndex("dbo.Lectures", new[] { "Course_CourseId" });
            DropIndex("dbo.Courses", new[] { "Artist_Id" });
            DropTable("dbo.Practices");
            DropTable("dbo.SubLectures");
            DropTable("dbo.Lectures");
            DropTable("dbo.Courses");
        }
    }
}
