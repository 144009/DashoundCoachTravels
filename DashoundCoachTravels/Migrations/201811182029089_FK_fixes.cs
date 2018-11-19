namespace DashoundCoachTravels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK_fixes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Coaches", "TripId", "dbo.Trips");
            DropIndex("dbo.Coaches", new[] { "TripId" });
            RenameColumn(table: "dbo.Reservations", name: "TripId", newName: "Id_Trip");
            RenameColumn(table: "dbo.Reservations", name: "UserId", newName: "Id_User");
            RenameColumn(table: "dbo.Trip_Location", name: "LocationId", newName: "Id_Location");
            RenameColumn(table: "dbo.Trip_Location", name: "TripId", newName: "Id_Trip");
            RenameIndex(table: "dbo.Reservations", name: "IX_TripId", newName: "IX_Id_Trip");
            RenameIndex(table: "dbo.Reservations", name: "IX_UserId", newName: "IX_Id_User");
            RenameIndex(table: "dbo.Trip_Location", name: "IX_TripId", newName: "IX_Id_Trip");
            RenameIndex(table: "dbo.Trip_Location", name: "IX_LocationId", newName: "IX_Id_Location");
            AddColumn("dbo.Trips", "CoachNumberId", c => c.Int(nullable: false));
            DropColumn("dbo.Coaches", "TripId");
            DropColumn("dbo.Trips", "CoachType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "CoachType", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Coaches", "TripId", c => c.Int(nullable: false));
            DropColumn("dbo.Trips", "CoachNumberId");
            RenameIndex(table: "dbo.Trip_Location", name: "IX_Id_Location", newName: "IX_LocationId");
            RenameIndex(table: "dbo.Trip_Location", name: "IX_Id_Trip", newName: "IX_TripId");
            RenameIndex(table: "dbo.Reservations", name: "IX_Id_User", newName: "IX_UserId");
            RenameIndex(table: "dbo.Reservations", name: "IX_Id_Trip", newName: "IX_TripId");
            RenameColumn(table: "dbo.Trip_Location", name: "Id_Trip", newName: "TripId");
            RenameColumn(table: "dbo.Trip_Location", name: "Id_Location", newName: "LocationId");
            RenameColumn(table: "dbo.Reservations", name: "Id_User", newName: "UserId");
            RenameColumn(table: "dbo.Reservations", name: "Id_Trip", newName: "TripId");
            CreateIndex("dbo.Coaches", "TripId");
            AddForeignKey("dbo.Coaches", "TripId", "dbo.Trips", "Id", cascadeDelete: true);
        }
    }
}
