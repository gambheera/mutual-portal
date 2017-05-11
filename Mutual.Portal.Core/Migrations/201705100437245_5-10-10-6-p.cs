namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _510106p : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CountViwedByMe", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "CountViwedByMe");
        }
    }
}
