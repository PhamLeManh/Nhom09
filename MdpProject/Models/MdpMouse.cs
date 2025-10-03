using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpMouse
{
    public int MdpMouseId { get; set; }

    public int MdpSanPhamId { get; set; }

    public string? MdpTenMouse { get; set; }

    public string? MdpKieuKetNoi { get; set; }

    public string? MdpDoPhanGiai { get; set; }

    public int? MdpSoNut { get; set; }

    public bool? MdpDenLed { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public DateTime? MdpUpdatedAt { get; set; }

    public string? MdpAnh { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;
}
