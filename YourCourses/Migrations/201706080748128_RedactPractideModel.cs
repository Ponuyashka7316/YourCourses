namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedactPractideModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Practices", "FirstPart", c => c.String(nullable: false));
            AddColumn("dbo.Practices", "TestsPart", c => c.String());
            AddColumn("dbo.Practices", "SecondPart", c => c.String());
            DropColumn("dbo.Practices", "TestsForUserInput");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Practices", "TestsForUserInput", c => c.String(nullable: false));
            DropColumn("dbo.Practices", "SecondPart");
            DropColumn("dbo.Practices", "TestsPart");
            DropColumn("dbo.Practices", "FirstPart");
        }
    }
}
