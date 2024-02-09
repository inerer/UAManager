using System;
using System.Collections.Generic;

namespace UAM.Core.Enteties;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
