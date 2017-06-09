namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Practices", "Mark", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Practices", "Mark");
        }
    }
}
