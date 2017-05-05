namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertIntoImage : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Images ON;");
            Sql("INSERT INTO Images (Id, Type) VALUES (1, 'Practice')");
            Sql("INSERT INTO Images (Id, Type) VALUES (2, 'Quiz')");
            Sql("INSERT INTO Images (Id, Type) VALUES (3, 'Lecture')");
            Sql("SET IDENTITY_INSERT Images OFF;");
        }
        
        public override void Down()
        {
            Sql("Delete from Images where id in (1, 2, 3)");
        }
    }
}
