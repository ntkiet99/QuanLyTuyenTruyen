namespace QuanLyThongTinTuyenTruyen.Models
{
    public class TrangThai
    {
        public TrangThai()
        {

        }
        public TrangThai(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}