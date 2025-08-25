using System;
using System.Collections.Generic;


namespace Student_Management.Models;

public partial class Hocsinh
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



    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual ICollection<Hocphi> Hocphis { get; set; } = new List<Hocphi>();

    public virtual Lop MaLopNavigation { get; set; } = null!;

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
