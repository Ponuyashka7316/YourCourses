namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatingForSublect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubLectures", "CurrentRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubLectures", "CurrentRating");
        }
    }
}
