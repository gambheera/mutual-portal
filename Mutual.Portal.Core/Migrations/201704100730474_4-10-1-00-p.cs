namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _410100p : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsRegistrationConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsRegistrationConfirmed");
        }
    }
}
