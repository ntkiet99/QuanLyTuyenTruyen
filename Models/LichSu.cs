using System;

namespace QuanLyThongTinTuyenTruyen.Models
{
    public class LichSu
    {
        public int LichSuId { get; set; }
        public DateTime NgayPhat { get; set; } = DateTime.Now;
        public int TrangThai { get; set; }
        public int SoLanPhat { get; set; }
        public int ThongTinId { get; set; }
        public ThongTin ThongTin { get; set; }
    }
}