using System;
using System.Collections.Generic;

namespace QuanLyThongTinTuyenTruyen.Models
{
    public class ThongTin
    {
        public int ThongTinId { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; } = DateTime.Now;
        public DateTime NgayHetHan { get; set; } = DateTime.Now;
        public string TenDonViSuDung { get; set; }
        public int TrangThai { get; set; } = 1;
        public ICollection<LichSu> LichSus { get; set; }
    }
}