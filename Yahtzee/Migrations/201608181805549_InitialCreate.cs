namespace Yahtzee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameResults",
                c => new
                    {
                        GameResultId = c.Int(nullable: false, identity: true),
                        Ones = c.Int(nullable: false),
                        Twos = c.Int(nullable: false),
                        Threes = c.Int(nullable: false),
                        Fours = c.Int(nullable: false),
                        Fives = c.Int(nullable: false),
                        Sixes = c.Int(nullable: false),
                        Tok = c.Int(nullable: false),
                        Fok = c.Int(nullable: false),
                        Fh = c.Int(nullable: false),
                        SmStr = c.Int(nullable: false),
                        LaStr = c.Int(nullable: false),
                        Yahtzee = c.Int(nullable: false),
                        Chance = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameResultId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameName = c.String(maxLength: 20),
                        TimeStamp = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        PlayerAId = c.String(nullable: false, maxLength: 128),
                        PlayerBId = c.String(nullable: false, maxLength: 128),
                        GameResultAId = c.Int(),
                        GameResultBId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameResults", t => t.GameResultAId)
                .ForeignKey("dbo.GameResults", t => t.GameResultBId)
                .ForeignKey("dbo.AspNetUsers", t => t.PlayerAId)
                .ForeignKey("dbo.AspNetUsers", t => t.PlayerBId)
                .Index(t => t.PlayerAId)
                .Index(t => t.PlayerBId)
                .Index(t => t.GameResultAId)
                .Index(t => t.GameResultBId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ScreenName = c.String(),
                        Avatar = c.String(),
                        PublicAccount = c.Boolean(nullable: false),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.GameResults", "GameId", "dbo.Games");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Games", "PlayerBId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Games", "PlayerAId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Games", "GameResultBId", "dbo.GameResults");
            DropForeignKey("dbo.Games", "GameResultAId", "dbo.GameResults");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Games", new[] { "GameResultBId" });
            DropIndex("dbo.Games", new[] { "GameResultAId" });
            DropIndex("dbo.Games", new[] { "PlayerBId" });
            DropIndex("dbo.Games", new[] { "PlayerAId" });
            DropIndex("dbo.GameResults", new[] { "GameId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Games");
            DropTable("dbo.GameResults");
        }
    }
}
