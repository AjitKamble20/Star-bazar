namespace StarBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPicodetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Pincode",
                c => new
                    {
                        PincodetId = c.Int(nullable: false, identity: true),
                        Pincode = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PincodetId)
                .ForeignKey("dbo.tbl_District", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_Pincode", "DistrictId", "dbo.tbl_District");
            DropIndex("dbo.tbl_Pincode", new[] { "DistrictId" });
            DropTable("dbo.tbl_Pincode");
        }
    }
}
