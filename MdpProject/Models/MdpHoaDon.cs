using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpHoaDon
{
    public int MdpHoaDonId { get; set; }

    public int MdpDonHangId { get; set; }

    public DateTime? MdpNgayXuat { get; set; }

    public decimal MdpTongTien { get; set; }

    public string? MdpPhuongThucThanhToan { get; set; }

    public string? MdpTrangThai { get; set; }

    public virtual ICollection<MdpChiTietHoaDon> MdpChiTietHoaDons { get; set; } = new List<MdpChiTietHoaDon>();

    public virtual MdpDonHang MdpDonHang { get; set; } = null!;
}
