namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_UploadsFileTb", "OtherRowId", c => c.Guid(nullable: false));
            DropColumn("dbo.DR_UploadsFileTb", "DisasterInfoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DR_UploadsFileTb", "DisasterInfoId", c => c.Guid(nullable: false));
            DropColumn("dbo.DR_UploadsFileTb", "OtherRowId");
        }
    }
}
