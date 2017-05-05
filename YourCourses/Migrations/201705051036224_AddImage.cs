namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SubLectures", "Image_Id", c => c.Int());
            CreateIndex("dbo.SubLectures", "Image_Id");
            AddForeignKey("dbo.SubLectures", "Image_Id", "dbo.Images", "Id");
            DropColumn("dbo.SubLectures", "SubImageType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubLectures", "SubImageType", c => c.Byte(nullable: false));
            DropForeignKey("dbo.SubLectures", "Image_Id", "dbo.Images");
            DropIndex("dbo.SubLectures", new[] { "Image_Id" });
            DropColumn("dbo.SubLectures", "Image_Id");
            DropTable("dbo.Images");
        }
    }
}
