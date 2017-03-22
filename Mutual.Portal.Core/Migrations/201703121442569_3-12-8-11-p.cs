namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _312811p : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Guid", c => c.Guid(nullable: false));
            DropColumn("dbo.Nurses", "District");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Nurses", "District", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Guid");
        }
    }
}
