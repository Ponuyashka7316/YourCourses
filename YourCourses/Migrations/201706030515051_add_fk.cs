namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_fk : DbMigration
    {
        public override void Up()
        {
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
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false, maxLength: 1000),
                        CourseInfo = c.String(maxLength: 2500),
                        DateOfCourseCreation = c.DateTime(nullable: false),
                        Artist_Id = c.String(nullable: false, maxLength: 128),
                        CourseType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id, cascadeDelete: true)
                .ForeignKey("dbo.CourseTypes", t => t.CourseType_Id, cascadeDelete: true)
                .Index(t => t.Artist_Id)
                .Index(t => t.CourseType_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.CourseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lectures",
                c => new
                    {
                        LectureId = c.Int(nullable: false, identity: true),
                        LectureName = c.String(nullable: false, maxLength: 1000),
                        CourseCourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LectureId)
                .ForeignKey("dbo.Courses", t => t.CourseCourseId, cascadeDelete: true)
                .Index(t => t.CourseCourseId);
            
            CreateTable(
                "dbo.SubLectures",
                c => new
                    {
                        SubId = c.Int(nullable: false, identity: true),
                        SubName = c.String(nullable: false, maxLength: 1000),
                        LectureAdminOutput = c.String(nullable: false),
                        Artist_Id = c.String(nullable: false, maxLength: 128),
                        Image_Id = c.Int(),
                        Lecture_LectureId = c.Int(),
                    })
                .PrimaryKey(t => t.SubId)
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .ForeignKey("dbo.Lectures", t => t.Lecture_LectureId)
                .Index(t => t.Artist_Id)
                .Index(t => t.Image_Id)
                .Index(t => t.Lecture_LectureId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Practices",
                c => new
                    {
                        PracticeId = c.Int(nullable: false, identity: true),
                        PracticeName = c.String(nullable: false, maxLength: 1000),
                        PracticeUserInput = c.String(),
                        TestsForUserInput = c.String(nullable: false),
                        Artist_Id = c.String(nullable: false, maxLength: 128),
                        SubLecture_SubId = c.Int(),
                    })
                .PrimaryKey(t => t.PracticeId)
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id, cascadeDelete: true)
                .ForeignKey("dbo.SubLectures", t => t.SubLecture_SubId)
                .Index(t => t.Artist_Id)
                .Index(t => t.SubLecture_SubId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SubLectures", "Lecture_LectureId", "dbo.Lectures");
            DropForeignKey("dbo.Practices", "SubLecture_SubId", "dbo.SubLectures");
            DropForeignKey("dbo.CorrectAnswers", "Practice_PracticeId", "dbo.Practices");
            DropForeignKey("dbo.Practices", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubLectures", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.SubLectures", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lectures", "CourseCourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CourseType_Id", "dbo.CourseTypes");
            DropForeignKey("dbo.Courses", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Practices", new[] { "SubLecture_SubId" });
            DropIndex("dbo.Practices", new[] { "Artist_Id" });
            DropIndex("dbo.SubLectures", new[] { "Lecture_LectureId" });
            DropIndex("dbo.SubLectures", new[] { "Image_Id" });
            DropIndex("dbo.SubLectures", new[] { "Artist_Id" });
            DropIndex("dbo.Lectures", new[] { "CourseCourseId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Courses", new[] { "CourseType_Id" });
            DropIndex("dbo.Courses", new[] { "Artist_Id" });
            DropIndex("dbo.CorrectAnswers", new[] { "Practice_PracticeId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Practices");
            DropTable("dbo.Images");
            DropTable("dbo.SubLectures");
            DropTable("dbo.Lectures");
            DropTable("dbo.CourseTypes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Courses");
            DropTable("dbo.CorrectAnswers");
        }
    }
}
