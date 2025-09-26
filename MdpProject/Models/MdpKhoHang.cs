using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpKhoHang
{
    public int MdpKhoId { get; set; }

    public int MdpSanPhamId { get; set; }

    public int? MdpSoLuongNhap { get; set; }

    public int? MdpSoLuongTon { get; set; }

    public DateTime? MdpNgayNhap { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;
}
