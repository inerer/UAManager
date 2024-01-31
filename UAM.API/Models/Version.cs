﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UAM.API.Models;

public partial class Version
{
    public Guid Id { get; set; }

    public string Build { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string Description { get; set; } = null!;

    public int Type { get; set; }

    public string Path { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<VersionDependency> VersionDependencies { get; set; } = new List<VersionDependency>();
}
