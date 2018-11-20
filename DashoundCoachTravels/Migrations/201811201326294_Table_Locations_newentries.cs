namespace DashoundCoachTravels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_Locations_newentries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "LocationImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "LocationImage");
        }
    }
}
