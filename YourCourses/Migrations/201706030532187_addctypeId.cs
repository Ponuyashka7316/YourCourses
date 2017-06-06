namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addctypeId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Courses", name: "CourseType_Id", newName: "CourseTypeId");
            RenameIndex(table: "dbo.Courses", name: "IX_CourseType_Id", newName: "IX_CourseTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Courses", name: "IX_CourseTypeId", newName: "IX_CourseType_Id");
            RenameColumn(table: "dbo.Courses", name: "CourseTypeId", newName: "CourseType_Id");
        }
    }
}
