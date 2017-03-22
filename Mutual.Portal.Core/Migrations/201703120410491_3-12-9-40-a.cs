namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _312940a : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Nurses", "Id", "dbo.Users");
            DropIndex("dbo.Nurses", new[] { "Id" });
            AddColumn("dbo.Users", "EmploymentType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "EmploymentType");
            CreateIndex("dbo.Nurses", "Id");
            AddForeignKey("dbo.Nurses", "Id", "dbo.Users", "Id");
        }
    }
}
