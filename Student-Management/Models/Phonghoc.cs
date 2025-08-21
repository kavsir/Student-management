using System;
using System.Collections.Generic;

namespace Student_Management.Models;

public partial class Phonghoc
{
    public int MaPhong { get; set; }

    public string TenPhong { get; set; } = null!;

    public int? SucChua { get; set; }

    public string? ViTri { get; set; }

    public virtual ICollection<Lichhoc> Lichhocs { get; set; } = new List<Lichhoc>();
}
