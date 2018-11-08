namespace DashoundCoachTravels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sampleData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reservations", new[] { "UserId" });
            AlterColumn("dbo.Reservations", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservations", "UserId");
            AddForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
