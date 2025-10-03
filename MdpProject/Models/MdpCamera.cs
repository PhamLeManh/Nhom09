using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpCamera
{
    public int MdpCameraId { get; set; }

    public int MdpSanPhamId { get; set; }

    public string? MdpTenCamera { get; set; }

    public string? MdpDoPhanGiai { get; set; }

    public string? MdpCamBien { get; set; }

    public string? MdpOngKinh { get; set; }

    public string? MdpBoNho { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public DateTime? MdpUpdatedAt { get; set; }

    public string? MdpAnh { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;
}
