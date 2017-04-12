namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4101119a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Code");
        }
    }
}
