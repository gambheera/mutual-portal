namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _412755p : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsWorkingPlaceDetailsProvided", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsWorkingPlaceDetailsProvided");
        }
    }
}
