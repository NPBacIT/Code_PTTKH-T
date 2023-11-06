using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace banvekhugiaitri_nhom4_pttkhdt.Models;

public partial class VeKhuVuiChoi
{
    public int MaVe { get; set; }

    public int? MaDaiLy { get; set; }

    public int? MaTp { get; set; }

    public string TenVe { get; set; } = null!;

    public TimeSpan MoCua { get; set; }

    public TimeSpan DongCua { get; set; }

    public string AnhKvc { get; set; } = null!;

    public decimal? Gia { get; set; }

    public int? Slvcl { get; set; }

    public int? Active { get; set; }

    public int? IsDeleted { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual DaiLy? MaDaiLyNavigation { get; set; }

    public virtual ThanhPho? MaTpNavigation { get; set; }
    [NotMapped]
    public IFormFile? formFile { get; set; }
}
