namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sublect : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubLectures", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubLectures", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Practices", "Artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubLectures", "Lecture_LectureId", "dbo.Lectures");
            DropIndex("dbo.SubLectures", new[] { "Artist_Id" });
            DropIndex("dbo.SubLectures", new[] { "Image_Id" });
            DropIndex("dbo.SubLectures", new[] { "Lecture_LectureId" });
            DropIndex("dbo.Practices", new[] { "Artist_Id" });
            RenameColumn(table: "dbo.SubLectures", name: "Lecture_LectureId", newName: "LectureLectureId");
            RenameColumn(table: "dbo.Practices", name: "SubLecture_SubId", newName: "Sublectures_SubId");
            RenameIndex(table: "dbo.Practices", name: "IX_SubLecture_SubId", newName: "IX_Sublectures_SubId");
            AlterColumn("dbo.SubLectures", "LectureLectureId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubLectures", "LectureLectureId");
            AddForeignKey("dbo.SubLectures", "LectureLectureId", "dbo.Lectures", "LectureId", cascadeDelete: true);
            DropColumn("dbo.SubLectures", "Artist_Id");
            DropColumn("dbo.SubLectures", "Image_Id");
            DropColumn("dbo.Practices", "Artist_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Practices", "Artist_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.SubLectures", "Image_Id", c => c.Int());
            AddColumn("dbo.SubLectures", "Artist_Id", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.SubLectures", "LectureLectureId", "dbo.Lectures");
            DropIndex("dbo.SubLectures", new[] { "LectureLectureId" });
            AlterColumn("dbo.SubLectures", "LectureLectureId", c => c.Int());
            RenameIndex(table: "dbo.Practices", name: "IX_Sublectures_SubId", newName: "IX_SubLecture_SubId");
            RenameColumn(table: "dbo.Practices", name: "Sublectures_SubId", newName: "SubLecture_SubId");
            RenameColumn(table: "dbo.SubLectures", name: "LectureLectureId", newName: "Lecture_LectureId");
            CreateIndex("dbo.Practices", "Artist_Id");
            CreateIndex("dbo.SubLectures", "Lecture_LectureId");
            CreateIndex("dbo.SubLectures", "Image_Id");
            CreateIndex("dbo.SubLectures", "Artist_Id");
            AddForeignKey("dbo.SubLectures", "Lecture_LectureId", "dbo.Lectures", "LectureId");
            AddForeignKey("dbo.Practices", "Artist_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SubLectures", "Image_Id", "dbo.Images", "Id");
            AddForeignKey("dbo.SubLectures", "Artist_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
