namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArtistToSublect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubLectures", "ArtistId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.SubLectures", "ArtistId");
            AddForeignKey("dbo.SubLectures", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubLectures", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.SubLectures", new[] { "ArtistId" });
            DropColumn("dbo.SubLectures", "ArtistId");
        }
    }
}
