using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpPc
{
    public int MdpPcid { get; set; }

    public int MdpSanPhamId { get; set; }

    public string MdpTenPc { get; set; } = null!;

    public string? MdpCpu { get; set; }

    public string? MdpGpu { get; set; }

    public string? MdpRam { get; set; }

    public string? MdpStorage { get; set; }

    public string? MdpMainboard { get; set; }

    public string? MdpPsu { get; set; }

    public string? MdpCaseType { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public DateTime? MdpUpdatedAt { get; set; }

    public string? MdpAnh { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;
}
