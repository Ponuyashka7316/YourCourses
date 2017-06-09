namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArtictFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "ArtistId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Quizs", "ArtistId");
            AddForeignKey("dbo.Quizs", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quizs", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Quizs", new[] { "ArtistId" });
            DropColumn("dbo.Quizs", "ArtistId");
        }
    }
}
