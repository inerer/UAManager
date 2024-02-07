﻿using System;
using System.Collections.Generic;

namespace UAM.Core.Enteties;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Problem> Problems { get; set; } = new List<Problem>();
}
