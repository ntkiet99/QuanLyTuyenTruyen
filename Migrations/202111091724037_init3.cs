namespace QuanLyThongTinTuyenTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LichSus", "SoLanPhat", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LichSus", "SoLanPhat");
        }
    }
}
