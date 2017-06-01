namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lect : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lectures", "Course_CourseId", "dbo.Courses");
            DropIndex("dbo.Lectures", new[] { "Course_CourseId" });
            AddColumn("dbo.Lectures", "Course_CourseId1", c => c.Int());
            AlterColumn("dbo.Lectures", "Course_CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lectures", "Course_CourseId1");
            AddForeignKey("dbo.Lectures", "Course_CourseId1", "dbo.Courses", "CourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "Course_CourseId1", "dbo.Courses");
            DropIndex("dbo.Lectures", new[] { "Course_CourseId1" });
            AlterColumn("dbo.Lectures", "Course_CourseId", c => c.Int());
            DropColumn("dbo.Lectures", "Course_CourseId1");
            CreateIndex("dbo.Lectures", "Course_CourseId");
            AddForeignKey("dbo.Lectures", "Course_CourseId", "dbo.Courses", "CourseId");
        }
    }
}
