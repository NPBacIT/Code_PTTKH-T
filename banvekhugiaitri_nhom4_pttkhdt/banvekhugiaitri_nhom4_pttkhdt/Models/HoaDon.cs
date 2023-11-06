using System;
using System.Collections.Generic;

namespace banvekhugiaitri_nhom4_pttkhdt.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public int? MaDaiLy { get; set; }

    public int? MaVe { get; set; }

    public int? MaKh { get; set; }

	public DateTime? NgayTao { get; set; }

	public int? SoLuong { get; set; }

	public decimal? TongTien { get; set; }

    public int? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Cthd> Cthds { get; set; } = new List<Cthd>();

    public virtual DaiLy? MaDaiLyNavigation { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual VeKhuVuiChoi? MaVeNavigation { get; set; }
}
