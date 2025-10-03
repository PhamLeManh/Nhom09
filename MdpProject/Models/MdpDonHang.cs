using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpDonHang
{
    public int MdpDonHangId { get; set; }

    public int MdpKhachHangId { get; set; }

    public DateTime? MdpNgayDatDonHang { get; set; }

    public DateTime? MdpNgayGiaoHang { get; set; }

    public string? MdpDiaChiGiaoHang { get; set; }

    public string? MdpPhuongThucThanhToan { get; set; }

    public string? MdpTrangThaiThanhToan { get; set; }

    public string? MdpStatus { get; set; }

    public decimal MdpTongTien { get; set; }

    public int? MdpKmid { get; set; }

    public string? MdpGhiChu { get; set; }

    public virtual ICollection<MdpChatSupport> MdpChatSupports { get; set; } = new List<MdpChatSupport>();

    public virtual ICollection<MdpChiTietDonHang> MdpChiTietDonHangs { get; set; } = new List<MdpChiTietDonHang>();

    public virtual ICollection<MdpHoaDon> MdpHoaDons { get; set; } = new List<MdpHoaDon>();

    public virtual MdpUser MdpKhachHang { get; set; } 

    public virtual MdpKhuyenMai? MdpKm { get; set; }
}
