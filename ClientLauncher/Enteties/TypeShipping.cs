using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class TypeShipping
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
