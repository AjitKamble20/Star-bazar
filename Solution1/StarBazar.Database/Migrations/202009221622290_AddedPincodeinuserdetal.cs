namespace StarBazar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPincodeinuserdetal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "PincodetId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDetails", "PincodetId");
        }
    }
}
