using System;
using System.Collections.Generic;

namespace Student_Management.Models;

public partial class PhancongGiangday
{
    public int MaPc { get; set; }

    public int MaGv { get; set; }

    public int MaMonHoc { get; set; }

    public int MaLop { get; set; }

    public int MaHk { get; set; }

    public virtual Giaovien MaGvNavigation { get; set; } = null!;

    public virtual Hocky MaHkNavigation { get; set; } = null!;

    public virtual Lop MaLopNavigation { get; set; } = null!;

    public virtual Monhoc MaMonHocNavigation { get; set; } = null!;
}
