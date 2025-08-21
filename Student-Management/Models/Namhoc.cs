using System;
using System.Collections.Generic;

namespace Student_Management.Models;

public partial class Namhoc
{
    public int MaNamHoc { get; set; }

    public string TenNamHoc { get; set; } = null!;

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public virtual ICollection<Hocky> Hockies { get; set; } = new List<Hocky>();

    public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();
}
