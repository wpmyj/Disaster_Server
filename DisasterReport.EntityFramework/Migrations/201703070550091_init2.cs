namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_MessageGroupTb", "Remark", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DR_MessageGroupTb", "Remark");
        }
    }
}
