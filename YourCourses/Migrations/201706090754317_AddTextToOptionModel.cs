namespace YourCourses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTextToOptionModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Options", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Options", "Text");
        }
    }
}
