using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpSanPham
{
    public int MdpSanPhamId { get; set; }

    public string MdpTenSanPham { get; set; } = null!;

    public string? MdpMoTa { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public DateTime? MdpUpdatedAt { get; set; }

    public virtual ICollection<MdpCamera> MdpCameras { get; set; } = new List<MdpCamera>();

    public virtual ICollection<MdpDoanhThuSanPham> MdpDoanhThuSanPhams { get; set; } = new List<MdpDoanhThuSanPham>();

    public virtual ICollection<MdpKhoHang> MdpKhoHangs { get; set; } = new List<MdpKhoHang>();

    public virtual ICollection<MdpLaptop> MdpLaptops { get; set; } = new List<MdpLaptop>();

    public virtual ICollection<MdpLinhKien> MdpLinhKiens { get; set; } = new List<MdpLinhKien>();

    public virtual ICollection<MdpMonitor> MdpMonitors { get; set; } = new List<MdpMonitor>();

    public virtual ICollection<MdpMouse> MdpMouses { get; set; } = new List<MdpMouse>();

    public virtual ICollection<MdpPc> MdpPcs { get; set; } = new List<MdpPc>();

    public virtual ICollection<MdpPhone> MdpPhones { get; set; } = new List<MdpPhone>();

    public virtual ICollection<MdpProductVariant> MdpProductVariants { get; set; } = new List<MdpProductVariant>();

    public virtual ICollection<MdpReview> MdpReviews { get; set; } = new List<MdpReview>();
}
