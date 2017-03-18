namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmessageNotefiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_MessageNoteTb", "Topic", c => c.String(unicode: false));
            AddColumn("dbo.DR_MessageNoteTb", "Summary", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DR_MessageNoteTb", "Summary");
            DropColumn("dbo.DR_MessageNoteTb", "Topic");
        }
    }
}
