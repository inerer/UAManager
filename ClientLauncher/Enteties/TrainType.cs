using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class TrainType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Train> Trains { get; set; } = new List<Train>();
}
