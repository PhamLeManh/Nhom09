using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpCategory
{
    public int MdpCategoryId { get; set; }

    public string MdpTenDanhMuc { get; set; } = null!;

    public string? MdpMoTa { get; set; }

    public string? MdpAnhDanhMuc { get; set; }

    public DateTime? MdpCreatedAt { get; set; }
}
