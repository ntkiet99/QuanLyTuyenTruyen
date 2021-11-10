namespace QuanLyThongTinTuyenTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LichSus",
                c => new
                    {
                        LichSuId = c.Int(nullable: false, identity: true),
                        NgayPhat = c.DateTime(nullable: false),
                        TrangThai = c.String(),
                        ThongTinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LichSuId)
                .ForeignKey("dbo.ThongTins", t => t.ThongTinId, cascadeDelete: true)
                .Index(t => t.ThongTinId);
            
            CreateTable(
                "dbo.ThongTins",
                c => new
                    {
                        ThongTinId = c.Int(nullable: false, identity: true),
                        Ten = c.String(),
                        MoTa = c.String(),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayHetHan = c.DateTime(nullable: false),
                        TenDonViSuDung = c.String(),
                        TrangThai = c.String(),
                    })
                .PrimaryKey(t => t.ThongTinId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LichSus", "ThongTinId", "dbo.ThongTins");
            DropIndex("dbo.LichSus", new[] { "ThongTinId" });
            DropTable("dbo.ThongTins");
            DropTable("dbo.LichSus");
        }
    }
}
