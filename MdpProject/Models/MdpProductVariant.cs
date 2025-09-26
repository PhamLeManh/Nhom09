using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpProductVariant
{
    public int MdpVariantId { get; set; }

    public int MdpSanPhamId { get; set; }

    public int? MdpMauSacId { get; set; }

    public string? MdpDungLuong { get; set; }

    public decimal? MdpGia { get; set; }

    public string? MdpAnh { get; set; }

    public int? MdpSoLuong { get; set; }

    public virtual ICollection<MdpCart> MdpCarts { get; set; } = new List<MdpCart>();

    public virtual ICollection<MdpChiTietDonHang> MdpChiTietDonHangs { get; set; } = new List<MdpChiTietDonHang>();

    public virtual ICollection<MdpChiTietHoaDon> MdpChiTietHoaDons { get; set; } = new List<MdpChiTietHoaDon>();

    public virtual ICollection<MdpDoanhThuSanPham> MdpDoanhThuSanPhams { get; set; } = new List<MdpDoanhThuSanPham>();

    public virtual MdpMauSac? MdpMauSac { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;
}
