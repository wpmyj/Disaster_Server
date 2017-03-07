namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uploadFiletb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DR_UploadsFileTb",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DisaterInfoId = c.Guid(nullable: false),
                        Path = c.String(unicode: false),
                        FileName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DR_UploadsFileTb");
        }
    }
}
