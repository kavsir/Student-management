using System;
using System.Collections.Generic;

namespace Student_Management.Models;

public partial class Lichhoc
{
    public int MaLichHoc { get; set; }

    public int MaLop { get; set; }

    public int MaMonHoc { get; set; }

    public int MaGv { get; set; }

    public int MaPhong { get; set; }

    public int MaHk { get; set; }

    public string? ThuTrongTuan { get; set; }

    public int? TietHoc { get; set; }

    public virtual Giaovien MaGvNavigation { get; set; } = null!;

    public virtual Hocky MaHkNavigation { get; set; } = null!;

    public virtual Lop MaLopNavigation { get; set; } = null!;

    public virtual Monhoc MaMonHocNavigation { get; set; } = null!;

    public virtual Phonghoc MaPhongNavigation { get; set; } = null!;
}
