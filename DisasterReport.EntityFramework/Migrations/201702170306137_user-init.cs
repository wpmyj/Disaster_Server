namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.dr_ReporterInfoTb",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(unicode: false),
                        Phone = c.String(unicode: false),
                        AreaCode = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.dr_UserTb",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserCode = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        Enable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.dr_UserTb");
            DropTable("dbo.dr_ReporterInfoTb");
        }
    }
}
