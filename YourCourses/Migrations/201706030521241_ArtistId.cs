namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtistId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Courses", name: "Artist_Id", newName: "ArtistId");
            RenameIndex(table: "dbo.Courses", name: "IX_Artist_Id", newName: "IX_ArtistId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Courses", name: "IX_ArtistId", newName: "IX_Artist_Id");
            RenameColumn(table: "dbo.Courses", name: "ArtistId", newName: "Artist_Id");
        }
    }
}
