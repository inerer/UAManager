using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class Rate
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? PricePerStation { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
