using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpReview
{
    public int MdpReviewId { get; set; }

    public int MdpSanPhamId { get; set; }

    public int MdpUserId { get; set; }

    public int? MdpRating { get; set; }

    public string? MdpComment { get; set; }

    public DateTime? MdpCreatedAt { get; set; }

    public virtual MdpSanPham MdpSanPham { get; set; } = null!;

    public virtual MdpUser MdpUser { get; set; } = null!;
}
