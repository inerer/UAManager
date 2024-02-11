using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class Wagon
{
    public int Id { get; set; }

    public int Capacity { get; set; }

    public int Number { get; set; }

    public string Series { get; set; } = null!;

    public int? TrainId { get; set; }

    public virtual Train? Train { get; set; }
}
