﻿using System;
using System.Collections.Generic;

namespace banvekhugiaitri_nhom4_pttkhdt.Models;

public partial class Cthd
{
    public int MaCthd { get; set; }

    public int MaHd { get; set; }

    public DateTime? NgayTao { get; set; }

    public decimal? Gia { get; set; }

    public int? SoVe { get; set; }

    public int? IsDeleted { get; set; }

    public virtual HoaDon MaHdNavigation { get; set; } = null!;
}
