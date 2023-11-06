using System;
using System.Collections.Generic;

namespace banvekhugiaitri_nhom4_pttkhdt.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string TenKh { get; set; } = null!;

    public string SoCmnd { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string AnhKh { get; set; } = null!;

    public int? IsDeleted { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
