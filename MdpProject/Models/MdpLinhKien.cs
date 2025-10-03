using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpLinhKien
{
    public int MdpLinhKienId { get; set; }

    public int MdpSanPhamId { get; set; }

    public string? MdpTenLinhKien { get; set; }

    public string? MdpLoaiLinhKien { get; set; }

    public string? MdpThongSoKyThuat { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public DateTime? MdpUpdatedAt { get; set; }

    public string? MdpAnh { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;
}
