namespace StarBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdddedLocationTble : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PincodeTables",
                c => new
                    {
                        PincodeId = c.Int(nullable: false, identity: true),
                        Pincode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PincodeId);
            
            CreateTable(
                "dbo.tbl_Location",
                c => new
                    {
                        DistrictId = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(nullable: false),
                        PincodeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistrictId)
                .ForeignKey("dbo.PincodeTables", t => t.PincodeId, cascadeDelete: true)
                .Index(t => t.PincodeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Location", "PincodeId", "dbo.PincodeTables");
            DropIndex("dbo.tbl_Location", new[] { "PincodeId" });
            DropTable("dbo.tbl_Location");
            DropTable("dbo.PincodeTables");
        }
    }
}
