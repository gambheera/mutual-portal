namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _414800a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DreamHospitals", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DreamHospitals", "IsActive");
        }
    }
}
