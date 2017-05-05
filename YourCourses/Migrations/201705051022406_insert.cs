namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insert : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT CourseTypes ON;");
            Sql("INSERT INTO CourseTypes (Id, Type) VALUES (1, 'Interactive')");
            Sql("INSERT INTO CourseTypes (Id, Type) VALUES (2, 'Video')");
            Sql("SET IDENTITY_INSERT CourseTypes OFF;");
        }

        public override void Down()
        {
            Sql("DELETE FROM CourseTypes WHERE Id IN (1, 2)");
        }
    }
}
