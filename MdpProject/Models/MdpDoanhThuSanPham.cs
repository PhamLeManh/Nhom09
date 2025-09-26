using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpDoanhThuSanPham
{
    public int MdpDoanhThuId { get; set; }

    public int MdpSanPhamId { get; set; }

    public int? MdpVariantId { get; set; }

    public int? MdpTongSoLuongBan { get; set; }

    public decimal? MdpTongDoanhThu { get; set; }

    public int? MdpThang { get; set; }

    public int? MdpNam { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;

    public virtual MdpProductVariant? MdpVariant { get; set; }
}
