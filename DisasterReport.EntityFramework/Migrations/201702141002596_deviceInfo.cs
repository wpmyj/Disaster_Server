namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deviceInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DR_DeviceInfoTb",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AreaCode = c.String(unicode: false),
                        DeviceCode = c.Int(nullable: false),
                        ReporterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DR_DeviceInfoTb");
        }
    }
}
