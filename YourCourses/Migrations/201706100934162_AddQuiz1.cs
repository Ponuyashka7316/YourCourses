namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuiz1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerChoices",
                c => new
                    {
                        AnswerChoiceId = c.Int(nullable: false, identity: true),
                        Choices = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerChoiceId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        AnswerId = c.Int(nullable: false),
                        Quiz_QuizId = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.Quizs", t => t.Quiz_QuizId)
                .Index(t => t.AnswerId)
                .Index(t => t.Quiz_QuizId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(),
                    })
                .PrimaryKey(t => t.AnswerId);
            
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
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        CourseTypeId = c.Int(nullable: false),
                        CourseName = c.String(nullable: false, maxLength: 1000),
                        CourseInfo = c.String(maxLength: 2500),
                        DateOfCourseCreation = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.CourseTypes", t => t.CourseTypeId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.CourseTypeId);
            
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
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        CurrentRating = c.Int(nullable: false),
                        SubName = c.String(nullable: false, maxLength: 1000),
                        LectureAdminOutput = c.String(nullable: false),
                        LectureLectureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubId)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId, cascadeDelete: false)
                .ForeignKey("dbo.Lectures", t => t.LectureLectureId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.LectureLectureId);
            
            CreateTable(
                "dbo.Practices",
                c => new
                    {
                        PracticeId = c.Int(nullable: false, identity: true),
                        PracticeName = c.String(nullable: false, maxLength: 1000),
                        PracticeUserInput = c.String(),
                        FirstPart = c.String(nullable: false),
                        TestsPart = c.String(),
                        SecondPart = c.String(),
                        Mark = c.Double(nullable: false),
                        SublectureSubId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PracticeId)
                .ForeignKey("dbo.SubLectures", t => t.SublectureSubId, cascadeDelete: true)
                .Index(t => t.SublectureSubId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuizId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(),
                        Duration = c.Time(precision: 7),
                        EndTime = c.DateTime(),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuizId);
            
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
            DropForeignKey("dbo.Questions", "Quiz_QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Practices", "SublectureSubId", "dbo.SubLectures");
            DropForeignKey("dbo.CorrectAnswers", "Practice_PracticeId", "dbo.Practices");
            DropForeignKey("dbo.SubLectures", "LectureLectureId", "dbo.Lectures");
            DropForeignKey("dbo.SubLectures", "ArtistId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lectures", "CourseCourseId", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CourseTypeId", "dbo.CourseTypes");
            DropForeignKey("dbo.Courses", "ArtistId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.AnswerChoices", "QuestionId", "dbo.Questions");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Practices", new[] { "SublectureSubId" });
            DropIndex("dbo.SubLectures", new[] { "LectureLectureId" });
            DropIndex("dbo.SubLectures", new[] { "ArtistId" });
            DropIndex("dbo.Lectures", new[] { "CourseCourseId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Courses", new[] { "CourseTypeId" });
            DropIndex("dbo.Courses", new[] { "ArtistId" });
            DropIndex("dbo.CorrectAnswers", new[] { "Practice_PracticeId" });
            DropIndex("dbo.Questions", new[] { "Quiz_QuizId" });
            DropIndex("dbo.Questions", new[] { "AnswerId" });
            DropIndex("dbo.AnswerChoices", new[] { "QuestionId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Quizs");
            DropTable("dbo.Images");
            DropTable("dbo.Practices");
            DropTable("dbo.SubLectures");
            DropTable("dbo.Lectures");
            DropTable("dbo.CourseTypes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Courses");
            DropTable("dbo.CorrectAnswers");
            DropTable("dbo.Answers");
            DropTable("dbo.Questions");
            DropTable("dbo.AnswerChoices");
        }
    }
}
