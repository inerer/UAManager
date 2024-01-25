using System;
using System.Collections.Generic;

namespace UAM.API.Models;

public partial class Dependency
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Version { get; set; } = null!;

    public virtual ICollection<VersionDependency> VersionDependencies { get; set; } = new List<VersionDependency>();
}
