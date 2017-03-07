namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disasterInfoTb_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_DisasterInfoTb", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.DR_DisasterInfoTb", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DR_DisasterInfoTb", "Status");
            DropColumn("dbo.DR_DisasterInfoTb", "Type");
        }
    }
}
