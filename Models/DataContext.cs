using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuanLyThongTinTuyenTruyen.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=ConnectionString")
        {
        }
        public DbSet<LichSu> LichSus { get; set; }
        public DbSet<ThongTin> ThongTin { get; set; }

    }
}