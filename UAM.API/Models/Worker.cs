﻿using System;
using System.Collections.Generic;

namespace UAM.API.Models;

public partial class Worker
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int? RoleId { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Problem> Problems { get; set; } = new List<Problem>();

    public virtual Role? Role { get; set; }
}
