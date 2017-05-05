namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Populate_Name_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courses", "CourseType_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "CourseType_Id");
            AddForeignKey("dbo.Courses", "CourseType_Id", "dbo.CourseTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.Courses", "CourseType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "CourseType", c => c.String(maxLength: 1000));
            DropForeignKey("dbo.Courses", "CourseType_Id", "dbo.CourseTypes");
            DropIndex("dbo.Courses", new[] { "CourseType_Id" });
            DropColumn("dbo.Courses", "CourseType_Id");
            DropTable("dbo.CourseTypes");
        }
    }
}
