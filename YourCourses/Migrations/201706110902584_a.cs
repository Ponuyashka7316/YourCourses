namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CorrectAnswers", name: "Practice_PracticeId", newName: "Practices_PracticeId");
            RenameIndex(table: "dbo.CorrectAnswers", name: "IX_Practice_PracticeId", newName: "IX_Practices_PracticeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CorrectAnswers", name: "IX_Practices_PracticeId", newName: "IX_Practice_PracticeId");
            RenameColumn(table: "dbo.CorrectAnswers", name: "Practices_PracticeId", newName: "Practice_PracticeId");
        }
    }
}
