namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disasterInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DR_DisasterInfoTb",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceCode = c.String(unicode: false),
                        ReportDate = c.DateTime(nullable: false, precision: 0),
                        ReporterId = c.Guid(nullable: false),
                        DisasterKindCode = c.String(unicode: false),
                        Lng = c.Double(nullable: false),
                        Lat = c.Double(nullable: false),
                        Remark = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DR_DisasterInfoTb");
        }
    }
}
