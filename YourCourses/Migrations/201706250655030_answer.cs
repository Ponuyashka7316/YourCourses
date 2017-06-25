namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CorrectAnswers", "Practices_PracticeId", "dbo.Practices");
            DropIndex("dbo.CorrectAnswers", new[] { "Practices_PracticeId" });
            RenameColumn(table: "dbo.CorrectAnswers", name: "Practices_PracticeId", newName: "PracticePracticeId");
            AlterColumn("dbo.CorrectAnswers", "PracticePracticeId", c => c.Int(nullable: false));
            CreateIndex("dbo.CorrectAnswers", "PracticePracticeId");
            AddForeignKey("dbo.CorrectAnswers", "PracticePracticeId", "dbo.Practices", "PracticeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CorrectAnswers", "PracticePracticeId", "dbo.Practices");
            DropIndex("dbo.CorrectAnswers", new[] { "PracticePracticeId" });
            AlterColumn("dbo.CorrectAnswers", "PracticePracticeId", c => c.Int());
            RenameColumn(table: "dbo.CorrectAnswers", name: "PracticePracticeId", newName: "Practices_PracticeId");
            CreateIndex("dbo.CorrectAnswers", "Practices_PracticeId");
            AddForeignKey("dbo.CorrectAnswers", "Practices_PracticeId", "dbo.Practices", "PracticeId");
        }
    }
}
