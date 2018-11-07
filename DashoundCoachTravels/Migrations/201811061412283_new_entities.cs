namespace DashoundCoachTravels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_entities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false, maxLength: 100),
                        Seats = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        VehicleNumber = c.Int(nullable: false),
                        TripId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .Index(t => t.TripId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        NumSpots = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        DateDeparture = c.DateTime(nullable: false),
                        DateBack = c.DateTime(nullable: false),
                        Description = c.String(),
                        BannerBig = c.String(),
                        BannerSmall = c.String(),
                        CoachType = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(nullable: false, maxLength: 100),
                        Town = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumPeople = c.Int(nullable: false),
                        DateBooked = c.DateTime(nullable: false),
                        Advance = c.Single(),
                        DatePayedAdvance = c.DateTime(),
                        DatePayedFull = c.DateTime(),
                        Status = c.String(nullable: false, maxLength: 100),
                        TripId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TripId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Trip_Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        TripId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .Index(t => t.TripId)
                .Index(t => t.LocationId);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Country", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Town", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Street", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "NumHouse", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "NumFlat", c => c.String());
            AddColumn("dbo.AspNetUsers", "ZIPCode", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trip_Location", "TripId", "dbo.Trips");
            DropForeignKey("dbo.Trip_Location", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "TripId", "dbo.Trips");
            DropForeignKey("dbo.Coaches", "TripId", "dbo.Trips");
            DropIndex("dbo.Trip_Location", new[] { "LocationId" });
            DropIndex("dbo.Trip_Location", new[] { "TripId" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Reservations", new[] { "TripId" });
            DropIndex("dbo.Coaches", new[] { "TripId" });
            DropColumn("dbo.AspNetUsers", "ZIPCode");
            DropColumn("dbo.AspNetUsers", "NumFlat");
            DropColumn("dbo.AspNetUsers", "NumHouse");
            DropColumn("dbo.AspNetUsers", "Street");
            DropColumn("dbo.AspNetUsers", "Town");
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.Trip_Location");
            DropTable("dbo.Reservations");
            DropTable("dbo.Locations");
            DropTable("dbo.Trips");
            DropTable("dbo.Coaches");
        }
    }
}
