using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class Train
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Series { get; set; } = null!;

    public int? TrainTypeId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual TrainType? TrainType { get; set; }

    public virtual ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();
}
