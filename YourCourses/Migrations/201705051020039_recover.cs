namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recover : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CourseTypes (Id, Type) VALUES (1, 'Interactive')");
            Sql("INSERT INTO CourseTypes (Id, Type) VALUES (2, 'Video')");
        }

        public override void Down()
        {
            Sql("DELETE FROM CourseTypes WHERE Id IN (1, 2)");
        }
    }
}
