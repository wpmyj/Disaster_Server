namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddisasterRescue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DR_DisasterRescueTb",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartTime = c.DateTime(nullable: false, precision: 0),
                        Disaster_Id = c.Guid(),
                        Reporter_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DR_DisasterInfoTb", t => t.Disaster_Id)
                .ForeignKey("dbo.DR_ReporterInfoTb", t => t.Reporter_Id)
                .Index(t => t.Disaster_Id)
                .Index(t => t.Reporter_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DR_DisasterRescueTb", "Reporter_Id", "dbo.DR_ReporterInfoTb");
            DropForeignKey("dbo.DR_DisasterRescueTb", "Disaster_Id", "dbo.DR_DisasterInfoTb");
            DropIndex("dbo.DR_DisasterRescueTb", new[] { "Reporter_Id" });
            DropIndex("dbo.DR_DisasterRescueTb", new[] { "Disaster_Id" });
            DropTable("dbo.DR_DisasterRescueTb");
        }
    }
}
