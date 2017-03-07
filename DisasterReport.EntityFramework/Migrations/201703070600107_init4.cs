namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_ReporterInfoTb", "Photo", c => c.String(unicode: false));
            AddColumn("dbo.DR_MessageGroupTb", "Photo", c => c.String(unicode: false));
            AddColumn("dbo.DR_MessageGroupTb", "Lable", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DR_MessageGroupTb", "Lable");
            DropColumn("dbo.DR_MessageGroupTb", "Photo");
            DropColumn("dbo.DR_ReporterInfoTb", "Photo");
        }
    }
}
