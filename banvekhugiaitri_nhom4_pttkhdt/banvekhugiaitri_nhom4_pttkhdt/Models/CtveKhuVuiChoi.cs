using System;
using System.Collections.Generic;

namespace banvekhugiaitri_nhom4_pttkhdt.Models;

public partial class CtveKhuVuiChoi
{
    public int MaCtve { get; set; }

    public int MaVe { get; set; }

    public string DiaDiem { get; set; } = null!;

    public string? UuDai { get; set; }

    public virtual VeKhuVuiChoi MaVeNavigation { get; set; } = null!;
}
