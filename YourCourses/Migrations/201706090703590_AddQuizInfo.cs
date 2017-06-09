namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuizInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "Info", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "Info");
        }
    }
}
