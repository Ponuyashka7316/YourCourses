namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Quiz_QuizId", "dbo.Quizs");
            DropIndex("dbo.Questions", new[] { "Quiz_QuizId" });
            RenameColumn(table: "dbo.Questions", name: "Quiz_QuizId", newName: "QuizQuizId");
            AlterColumn("dbo.Questions", "QuizQuizId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "QuizQuizId");
            AddForeignKey("dbo.Questions", "QuizQuizId", "dbo.Quizs", "QuizId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "QuizQuizId", "dbo.Quizs");
            DropIndex("dbo.Questions", new[] { "QuizQuizId" });
            AlterColumn("dbo.Questions", "QuizQuizId", c => c.Int());
            RenameColumn(table: "dbo.Questions", name: "QuizQuizId", newName: "Quiz_QuizId");
            CreateIndex("dbo.Questions", "Quiz_QuizId");
            AddForeignKey("dbo.Questions", "Quiz_QuizId", "dbo.Quizs", "QuizId");
        }
    }
}
