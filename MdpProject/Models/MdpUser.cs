using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpUser
{
    public int MdpUserId { get; set; }

    public string MdpTenDangNhap { get; set; } = null!;

    public string MdpMatKhau { get; set; } = null!;

    public string MdpEmail { get; set; } = null!;

    public string MdpHoTen { get; set; } = null!;

    public string? MdpSoDienThoai { get; set; }

    public string? MdpDiaChi { get; set; }

    public string? MdpRole { get; set; }

    public DateOnly? MdpNgaySinh { get; set; }

    public string? MdpGioiTinh { get; set; }

    public string? MdpAvatar { get; set; }

    public string? MdpTrangThai { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public DateTime? MdpUpdatedAt { get; set; }

    public virtual ICollection<MdpCart> MdpCarts { get; set; } = new List<MdpCart>();

    public virtual ICollection<MdpChatSupport> MdpChatSupports { get; set; } = new List<MdpChatSupport>();

    public virtual ICollection<MdpDonHang> MdpDonHangs { get; set; } = new List<MdpDonHang>();

    public virtual ICollection<MdpReview> MdpReviews { get; set; } = new List<MdpReview>();
}
