using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpCart
{
    public int MdpCartId { get; set; }

    public int MdpUserId { get; set; }

    public int MdpVariantId { get; set; }

    public int? MdpSoLuong { get; set; }

    public DateTime? MdpNgayThem { get; set; }

    public virtual MdpUser MdpUser { get; set; } = null!;

    public virtual MdpProductVariant MdpVariant { get; set; } = null!;
}
