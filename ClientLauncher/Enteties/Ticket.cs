using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class Ticket
{
    public int Id { get; set; }

    public int? TrainId { get; set; }

    public int? ClientInfoId { get; set; }

    public int? TypeShippingId { get; set; }

    public DateTime? DateDeparture { get; set; }

    public DateTime? DateArrival { get; set; }

    public DateTime? DateRegistration { get; set; }

    public int? RateId { get; set; }

    public int? CountStation { get; set; }

    public int? WorkerId { get; set; }

    public decimal? Price { get; set; }

    public virtual ClientInfo? ClientInfo { get; set; }

    public virtual Rate? Rate { get; set; }

    public virtual Train? Train { get; set; }

    public virtual TypeShipping? TypeShipping { get; set; }

    public virtual Worker? Worker { get; set; }
}
