namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_ReporterInfoTb", "LastAddress", c => c.String(unicode: false));
            AddColumn("dbo.DR_ReporterInfoTb", "LastLng", c => c.Double(nullable: false));
            AddColumn("dbo.DR_ReporterInfoTb", "LastLat", c => c.Double(nullable: false));
            DropColumn("dbo.DR_ReporterInfoTb", "LastLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DR_ReporterInfoTb", "LastLocation", c => c.String(unicode: false));
            DropColumn("dbo.DR_ReporterInfoTb", "LastLat");
            DropColumn("dbo.DR_ReporterInfoTb", "LastLng");
            DropColumn("dbo.DR_ReporterInfoTb", "LastAddress");
        }
    }
}
