namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class weere : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserModels", "DateOfBirth", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserModels", "DateOfBirth");
        }
    }
}
