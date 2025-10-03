using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpMonitor
{
    public int MdpMonitorId { get; set; }

    public int MdpSanPhamId { get; set; }

    public string? MdpTenMonitor { get; set; }

    public string? MdpKichThuoc { get; set; }

    public string? MdpDoPhanGiai { get; set; }

    public string? MdpTanSoQuet { get; set; }

    public string? MdpCongKetNoi { get; set; }

    public string? MdpTamNen { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public DateTime? MdpUpdatedAt { get; set; }

    public string? MdpAnh { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;
}
