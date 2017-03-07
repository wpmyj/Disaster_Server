namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DR_DevBindReporterTb",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DeviceCode = c.Int(nullable: false),
                        ReporterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DR_MessageNoteTb",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FromUserId = c.Guid(nullable: false),
                        ToUserId = c.Guid(nullable: false),
                        Text = c.String(unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        Flag = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DR_MessageNoteTb");
            DropTable("dbo.DR_DevBindReporterTb");
        }
    }
}
