namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _510109p : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "MyRemainingViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "MyRemainingViewCount");
        }
    }
}
