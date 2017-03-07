namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class disasterKindinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DR_DisasterKindTb",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(unicode: false),
                        KindCode = c.String(unicode: false),
                        Pid = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DR_DisasterKindTb");
        }
    }
}
