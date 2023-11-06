using System;
using System.Collections.Generic;

namespace banvekhugiaitri_nhom4_pttkhdt.Models;

public partial class ThanhPho
{
    public int MaTp { get; set; }

    public string TenTp { get; set; } = null!;

    public virtual ICollection<VeKhuVuiChoi> VeKhuVuiChois { get; set; } = new List<VeKhuVuiChoi>();
}
