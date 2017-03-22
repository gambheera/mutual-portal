namespace Mutual.Portal.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _312300p : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DreamHospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hospital_Id = c.Int(nullable: false),
                        Nurse_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hospitals", t => t.Hospital_Id)
                .ForeignKey("dbo.Nurses", t => t.Nurse_Id)
                .Index(t => t.Hospital_Id)
                .Index(t => t.Nurse_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DreamHospitals", "Nurse_Id", "dbo.Nurses");
            DropForeignKey("dbo.DreamHospitals", "Hospital_Id", "dbo.Hospitals");
            DropIndex("dbo.DreamHospitals", new[] { "Nurse_Id" });
            DropIndex("dbo.DreamHospitals", new[] { "Hospital_Id" });
            DropTable("dbo.DreamHospitals");
        }
    }
}
