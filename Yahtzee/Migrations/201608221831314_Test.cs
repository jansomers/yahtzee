namespace Yahtzee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScreenName = c.String(),
                        Message = c.String(),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.User_Friends",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        User_Id1 = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.User_Id, t.User_Id1 })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id1)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            AddColumn("dbo.Games", "ScoreA", c => c.Int());
            AddColumn("dbo.Games", "ScoreB", c => c.Int());
            AddColumn("dbo.Games", "WinnerId", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Friends", "User_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.User_Friends", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChatMessages", "GameId", "dbo.Games");
            DropIndex("dbo.User_Friends", new[] { "User_Id1" });
            DropIndex("dbo.User_Friends", new[] { "User_Id" });
            DropIndex("dbo.ChatMessages", new[] { "GameId" });
            DropColumn("dbo.Games", "WinnerId");
            DropColumn("dbo.Games", "ScoreB");
            DropColumn("dbo.Games", "ScoreA");
            DropTable("dbo.User_Friends");
            DropTable("dbo.ChatMessages");
        }
    }
}
