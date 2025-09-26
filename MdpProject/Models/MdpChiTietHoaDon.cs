using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpChiTietHoaDon
{
    public int MdpChiTietHdid { get; set; }

    public int MdpHoaDonId { get; set; }

    public int MdpVariantId { get; set; }

    public int MdpSoLuong { get; set; }

    public decimal MdpGia { get; set; }

    public decimal? MdpGiamGia { get; set; }

    public decimal? MdpThanhTien { get; set; }

    public virtual MdpHoaDon MdpHoaDon { get; set; } = null!;

    public virtual MdpProductVariant MdpVariant { get; set; } = null!;
}
