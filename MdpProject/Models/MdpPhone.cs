using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpPhone
{
    public int MdpPhoneId { get; set; }

    public int MdpSanPhamId { get; set; }

    public string MdpTenPhone { get; set; } = null!;

    public string? MdpManHinh { get; set; }

    public string? MdpCamera { get; set; }

    public string? MdpPin { get; set; }

    public string? MdpRam { get; set; }

    public string? MdpStorage { get; set; }

    public string? MdpChipset { get; set; }

    public string? MdpHeDieuHanh { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public DateTime? MdpUpdatedAt { get; set; }

    public string? MdpAnh { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;
}
