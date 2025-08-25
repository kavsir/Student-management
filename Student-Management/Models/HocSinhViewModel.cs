
namespace Student_Management.Models.ViewModels
{
    public class HocSinhViewModel
    {
        public int MaHs { get; set; }
        public string HoTen { get; set; } = null!;
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? Email { get; set; }
        public string? Sdt { get; set; }
        public string? TrangThai { get; set; }
        public string? DiaChi { get; set; }
        public int MaLop { get; set; }
        public string? LopTen { get; set; }
        public List<Lop> LopList { get; internal set; }
    }
}
