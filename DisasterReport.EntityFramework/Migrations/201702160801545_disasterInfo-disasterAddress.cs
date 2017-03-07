namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disasterInfodisasterAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_DisasterInfoTb", "DisasterAddress", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DR_DisasterInfoTb", "DisasterAddress");
        }
    }
}
