namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _323853a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SocialId", c => c.String(nullable: false));
            AddColumn("dbo.Users", "SocialAccountProvider", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "MyCurrentViewCount", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "MyTotalViewCount", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "FacebookId");
            DropColumn("dbo.Users", "GoogleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "GoogleId", c => c.String());
            AddColumn("dbo.Users", "FacebookId", c => c.String());
            DropColumn("dbo.Users", "MyTotalViewCount");
            DropColumn("dbo.Users", "MyCurrentViewCount");
            DropColumn("dbo.Users", "SocialAccountProvider");
            DropColumn("dbo.Users", "SocialId");
        }
    }
}
