using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class Subscription
{
    public int Id { get; set; }

    public int? ClientInfoId { get; set; }

    public int? RateId { get; set; }

    public int? CountStation { get; set; }

    public decimal? Price { get; set; }

    public virtual ClientInfo? ClientInfo { get; set; }

    public virtual Rate? Rate { get; set; }
}
