namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class begg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductNewEndUsers",
                c => new
                    {
                        ProductNew_ProductID = c.Int(nullable: false),
                        EndUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductNew_ProductID, t.EndUser_ID })
                .ForeignKey("dbo.ProductNews", t => t.ProductNew_ProductID, cascadeDelete: true)
                .ForeignKey("dbo.EveryBody", t => t.EndUser_ID, cascadeDelete: false)
                .Index(t => t.ProductNew_ProductID)
                .Index(t => t.EndUser_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductNewEndUsers", "EndUser_ID", "dbo.EveryBody");
            DropForeignKey("dbo.ProductNewEndUsers", "ProductNew_ProductID", "dbo.ProductNews");
            DropIndex("dbo.ProductNewEndUsers", new[] { "EndUser_ID" });
            DropIndex("dbo.ProductNewEndUsers", new[] { "ProductNew_ProductID" });
            DropTable("dbo.ProductNewEndUsers");
        }
    }
}
