using System;
using System.Collections.Generic;

namespace banvekhugiaitri_nhom4_pttkhdt.Models;

public partial class AnhKhuVuiChoi
{
    public int MaVe { get; set; }

    public string TenFileAnh { get; set; } = null!;

    public string ViTri { get; set; } = null!;

    public virtual VeKhuVuiChoi MaVeNavigation { get; set; } = null!;
}
