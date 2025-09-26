using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpKhuyenMai
{
    public int MdpKmid { get; set; }

    public string? MdpMaCode { get; set; }

    public string? MdpMoTa { get; set; }

    public decimal? MdpGiaTri { get; set; }

    public int? MdpPhanTram { get; set; }

    public DateTime? MdpNgayBatDau { get; set; }

    public DateTime? MdpNgayKetThuc { get; set; }

    public string? MdpTrangThai { get; set; }

    public virtual ICollection<MdpDonHang> MdpDonHangs { get; set; } = new List<MdpDonHang>();
}
