namespace QuanLyThongTinTuyenTruyen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LichSus", "TrangThai", c => c.Int(nullable: false));
            AlterColumn("dbo.ThongTins", "TrangThai", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ThongTins", "TrangThai", c => c.String());
            AlterColumn("dbo.LichSus", "TrangThai", c => c.String());
        }
    }
}
