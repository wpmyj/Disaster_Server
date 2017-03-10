namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_DeviceInfoTb", "Reporter_Id", c => c.Guid());
            CreateIndex("dbo.DR_DeviceInfoTb", "Reporter_Id");
            AddForeignKey("dbo.DR_DeviceInfoTb", "Reporter_Id", "dbo.DR_ReporterInfoTb", "Id");
            DropColumn("dbo.DR_DeviceInfoTb", "ReporterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DR_DeviceInfoTb", "ReporterId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.DR_DeviceInfoTb", "Reporter_Id", "dbo.DR_ReporterInfoTb");
            DropIndex("dbo.DR_DeviceInfoTb", new[] { "Reporter_Id" });
            DropColumn("dbo.DR_DeviceInfoTb", "Reporter_Id");
        }
    }
}
