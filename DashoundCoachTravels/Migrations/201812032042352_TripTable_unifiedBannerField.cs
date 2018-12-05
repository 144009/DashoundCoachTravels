namespace DashoundCoachTravels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TripTable_unifiedBannerField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "Banner", c => c.String());
            DropColumn("dbo.Trips", "BannerBig");
            DropColumn("dbo.Trips", "BannerSmall");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trips", "BannerSmall", c => c.String());
            AddColumn("dbo.Trips", "BannerBig", c => c.String());
            DropColumn("dbo.Trips", "Banner");
        }
    }
}
