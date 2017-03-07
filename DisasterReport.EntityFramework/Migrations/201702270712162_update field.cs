namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_UploadsFileTb", "DisasterInfoId", c => c.Guid(nullable: false));
            DropColumn("dbo.DR_UploadsFileTb", "DisaterInfoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DR_UploadsFileTb", "DisaterInfoId", c => c.Guid(nullable: false));
            DropColumn("dbo.DR_UploadsFileTb", "DisasterInfoId");
        }
    }
}
