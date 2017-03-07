namespace DisasterReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DR_DisasterInfoTb", "DisasterKind_Id", c => c.Guid());
            CreateIndex("dbo.DR_DisasterInfoTb", "DisasterKind_Id");
            AddForeignKey("dbo.DR_DisasterInfoTb", "DisasterKind_Id", "dbo.DR_DisasterKindTb", "Id");
            DropColumn("dbo.DR_DisasterInfoTb", "DisasterKindCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DR_DisasterInfoTb", "DisasterKindCode", c => c.String(unicode: false));
            DropForeignKey("dbo.DR_DisasterInfoTb", "DisasterKind_Id", "dbo.DR_DisasterKindTb");
            DropIndex("dbo.DR_DisasterInfoTb", new[] { "DisasterKind_Id" });
            DropColumn("dbo.DR_DisasterInfoTb", "DisasterKind_Id");
        }
    }
}
