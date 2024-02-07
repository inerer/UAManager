using System;
using System.Collections.Generic;

namespace UAM.Core.Enteties;

public partial class VersionDependency
{
    public int Id { get; set; }

    public Guid VersionId { get; set; }

    public Guid DependenciesId { get; set; }

    public virtual Dependency Dependencies { get; set; } = null!;

    public virtual Version Version { get; set; } = null!;
}
