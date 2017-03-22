namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _312926a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FacebookId = c.String(),
                        GoogleId = c.String(),
                        RegisteredOn = c.DateTime(nullable: false),
                        LastLoginOn = c.DateTime(nullable: false),
                        ContactNumber1 = c.String(),
                        ContactNumber2 = c.String(),
                        Email = c.String(),
                        State = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Nurses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        District = c.Int(nullable: false),
                        Hospital_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hospitals", t => t.Hospital_Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Hospital_Id);
            
            CreateTable(
                "dbo.Hospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        District = c.Int(nullable: false),
                        Category = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Nurses", "Id", "dbo.Users");
            DropForeignKey("dbo.Nurses", "Hospital_Id", "dbo.Hospitals");
            DropIndex("dbo.Nurses", new[] { "Hospital_Id" });
            DropIndex("dbo.Nurses", new[] { "Id" });
            DropTable("dbo.Hospitals");
            DropTable("dbo.Nurses");
            DropTable("dbo.Users");
        }
    }
}
