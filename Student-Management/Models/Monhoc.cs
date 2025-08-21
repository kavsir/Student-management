using System;
using System.Collections.Generic;

namespace Student_Management.Models;

public partial class Monhoc
{
    public int MaMonHoc { get; set; }

    public string TenMonHoc { get; set; } = null!;

    public int? SoTiet { get; set; }

    public double? HeSo { get; set; }

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual ICollection<Giaovien> Giaoviens { get; set; } = new List<Giaovien>();

    public virtual ICollection<Lichhoc> Lichhocs { get; set; } = new List<Lichhoc>();

    public virtual ICollection<PhancongGiangday> PhancongGiangdays { get; set; } = new List<PhancongGiangday>();
}
