using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpLaptop
{
    public int MdpLaptopId { get; set; }

    public int MdpSanPhamId { get; set; }

    public string MdpTenLaptop { get; set; } = null!;

    public string? MdpCpu { get; set; }

    public string? MdpRam { get; set; }

    public string? MdpStorage { get; set; }

    public string? MdpGpu { get; set; }

    public string? MdpManHinh { get; set; }

    public string? MdpPin { get; set; }

    public string? MdpHeDieuHanh { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public DateTime? MdpUpdatedAt { get; set; }

    public string? MdpAnh { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;
}
