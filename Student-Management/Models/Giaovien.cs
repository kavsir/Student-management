using System;
using System.Collections.Generic;

namespace Student_Management.Models;

public partial class Giaovien
{
    public int MaGv { get; set; }

    public string HoTen { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    public string? Sdt { get; set; }

    public string? Email { get; set; }

    public int? MaMonHoc { get; set; }

    public virtual ICollection<Lichhoc> Lichhocs { get; set; } = new List<Lichhoc>();

    public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();

    public virtual Monhoc? MaMonHocNavigation { get; set; }

    public virtual ICollection<PhancongGiangday> PhancongGiangdays { get; set; } = new List<PhancongGiangday>();

    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
