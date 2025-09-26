using System;
using System.Collections.Generic;

namespace MdpProject.Models;

public partial class MdpChatSupport
{
    public int MdpChatId { get; set; }

    public int MdpDonHangId { get; set; }

    public int MdpSenderId { get; set; }

    public string MdpTinNhan { get; set; } = null!;

    public DateTime? MdpSentAt { get; set; }

    public string? MdpFileDinhKem { get; set; }

    public bool? MdpDaXem { get; set; }

    public virtual MdpDonHang MdpDonHang { get; set; } = null!;

    public virtual MdpUser MdpSender { get; set; } = null!;
}
