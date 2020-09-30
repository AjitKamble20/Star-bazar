namespace StarBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcolumninlocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Location", "Pincode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Location", "Pincode");
        }
    }
}
