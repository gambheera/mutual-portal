namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _312942a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nurses", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Nurses", "User_Id");
            AddForeignKey("dbo.Nurses", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nurses", "User_Id", "dbo.Users");
            DropIndex("dbo.Nurses", new[] { "User_Id" });
            DropColumn("dbo.Nurses", "User_Id");
        }
    }
}
