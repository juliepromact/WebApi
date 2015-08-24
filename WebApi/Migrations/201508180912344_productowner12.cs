namespace WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productowner12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EveryBody",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        DateOfJoining = c.DateTime(),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        PIN = c.Int(nullable: false),
                        ContactNumber = c.Int(nullable: false),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(),
                        OwnerName = c.String(),
                        CompanyName = c.String(),
                        FoundedIn = c.DateTime(),
                        Description = c.String(),
                        WebsiteURL = c.String(),
                        TwitterHandler = c.String(),
                        FacebookPageURL = c.String(),
                        Approval = c.Boolean(),
                        EmailConfirmed = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        MediaID = c.Int(nullable: false, identity: true),
                        VideoUrl = c.String(),
                        MediaName = c.String(),
                        AlternateText = c.String(),
                        ContentType = c.String(),
                        ImageData = c.Binary(),
                        Discriminator = c.String(),
                        Update_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MediaID)
                .ForeignKey("dbo.Updates", t => t.Update_ID, cascadeDelete: true)
                .Index(t => t.Update_ID);
            
            CreateTable(
                "dbo.Updates",
                c => new
                    {
                        UpdateID = c.Int(nullable: false, identity: true),
                        UpdateIntro = c.String(),
                        UpdateDetail = c.String(),
                        UpdateDate = c.DateTime(nullable: false),
                        Product_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UpdateID)
                .ForeignKey("dbo.ProductNews", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.ProductNews",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductOwner_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.EveryBody", t => t.ProductOwner_ID, cascadeDelete: true)
                .Index(t => t.ProductOwner_ID);
            
            CreateTable(
                "dbo.OwnerRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        CompanyName = c.String(),
                        OwnerName = c.String(),
                        Approval = c.Boolean(),
                        EmailConfirmed = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ClientId = c.String(nullable: false, maxLength: 50),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        DateOfJoining = c.DateTime(),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        PIN = c.Int(nullable: false),
                        ContactNumber = c.Int(nullable: false),
                        FoundedIn = c.DateTime(),
                        Description = c.String(),
                        WebsiteURL = c.String(),
                        TwitterHandler = c.String(),
                        FacebookPageURL = c.String(),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        EndUser_ID = c.Int(),
                        ProductOwner_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EveryBody", t => t.EndUser_ID)
                .ForeignKey("dbo.EveryBody", t => t.ProductOwner_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.EndUser_ID)
                .Index(t => t.ProductOwner_ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ProductOwner_ID", "dbo.EveryBody");
            DropForeignKey("dbo.AspNetUsers", "EndUser_ID", "dbo.EveryBody");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Media", "Update_ID", "dbo.Updates");
            DropForeignKey("dbo.Updates", "Product_ID", "dbo.ProductNews");
            DropForeignKey("dbo.ProductNews", "ProductOwner_ID", "dbo.EveryBody");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "ProductOwner_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "EndUser_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProductNews", new[] { "ProductOwner_ID" });
            DropIndex("dbo.Updates", new[] { "Product_ID" });
            DropIndex("dbo.Media", new[] { "Update_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.OwnerRequests");
            DropTable("dbo.ProductNews");
            DropTable("dbo.Updates");
            DropTable("dbo.Media");
            DropTable("dbo.EveryBody");
            DropTable("dbo.Clients");
        }
    }
}
