using System;
using System.Collections.Generic;

namespace Student_Management.Models;

public partial class Lop
{
    public int MaLop { get; set; }

    public string TenLop { get; set; } = null!;

    public int? SiSo { get; set; }

    public int MaNamHoc { get; set; }

    public int? MaGvcn { get; set; }

    public virtual ICollection<Hocsinh> Hocsinhs { get; set; } = new List<Hocsinh>();

    public virtual ICollection<Lichhoc> Lichhocs { get; set; } = new List<Lichhoc>();

    public virtual Giaovien? MaGvcnNavigation { get; set; }

    public virtual ICollection<PhancongGiangday> PhancongGiangdays { get; set; } = new List<PhancongGiangday>();
}
