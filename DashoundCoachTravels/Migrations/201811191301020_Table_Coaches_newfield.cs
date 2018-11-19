namespace DashoundCoachTravels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_Coaches_newfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coaches", "VehModel", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coaches", "VehModel");
        }
    }
}
