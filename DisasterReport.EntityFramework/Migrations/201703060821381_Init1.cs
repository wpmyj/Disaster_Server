namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_ReporterInfoTb", "LastLocation", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DR_ReporterInfoTb", "LastLocation");
        }
    }
}
