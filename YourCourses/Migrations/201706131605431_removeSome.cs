namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeSome : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnswerChoices", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Questions", "QuizQuizId", "dbo.Quizs");
            DropIndex("dbo.AnswerChoices", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "AnswerId" });
            DropIndex("dbo.Questions", new[] { "QuizQuizId" });
            DropTable("dbo.AnswerChoices");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
            DropTable("dbo.Quizs");
        }
        
        public override void Down()
        {
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
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(),
                    })
                .PrimaryKey(t => t.AnswerId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        AnswerId = c.Int(nullable: false),
                        QuizQuizId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId);
            
            CreateTable(
                "dbo.AnswerChoices",
                c => new
                    {
                        AnswerChoiceId = c.Int(nullable: false, identity: true),
                        Choices = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerChoiceId);
            
            CreateIndex("dbo.Questions", "QuizQuizId");
            CreateIndex("dbo.Questions", "AnswerId");
            CreateIndex("dbo.AnswerChoices", "QuestionId");
            AddForeignKey("dbo.Questions", "QuizQuizId", "dbo.Quizs", "QuizId", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "AnswerId", "dbo.Answers", "AnswerId", cascadeDelete: true);
            AddForeignKey("dbo.AnswerChoices", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: true);
        }
    }
}
