namespace DashoundCoachTravels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableCoachesUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coaches", "VehScreenshot", c => c.String());
            AlterColumn("dbo.Reservations", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservations", "UserId");
            AddForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "UserId" });
            AlterColumn("dbo.Reservations", "UserId", c => c.String());
            DropColumn("dbo.Coaches", "VehScreenshot");
        }
    }
}
