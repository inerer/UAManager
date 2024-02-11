﻿using System;
using System.Collections.Generic;

namespace UAManagerWeb.Models;

public partial class Priority
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Problem> Problems { get; set; } = new List<Problem>();
}
