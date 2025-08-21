using System;
using System.Collections.Generic;

namespace Student_Management.Models;

public partial class Diem
{
    public int MaDiem { get; set; }

    public int MaHs { get; set; }

    public int MaMonHoc { get; set; }

    public int MaHk { get; set; }

    public double? DiemMieng { get; set; }

    public double? Diem15p { get; set; }

    public double? Diem1Tiet { get; set; }

    public double? DiemThi { get; set; }

    public double? DiemTb { get; set; }

    public virtual Hocky MaHkNavigation { get; set; } = null!;

    public virtual Hocsinh MaHsNavigation { get; set; } = null!;

    public virtual Monhoc MaMonHocNavigation { get; set; } = null!;
}
