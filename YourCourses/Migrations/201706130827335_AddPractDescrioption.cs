namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPractDescrioption : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Practices", "PracticeDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Practices", "PracticeDescription");
        }
    }
}
