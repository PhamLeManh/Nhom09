using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpMauSac
{
    public int MdpMauSacId { get; set; }

    public string? MdpTenMau { get; set; }

    public string? MdpMaHex { get; set; }

    public virtual ICollection<MdpProductVariant> MdpProductVariants { get; set; } = new List<MdpProductVariant>();
}
