using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class Citizenship
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PassportInfo> PassportInfos { get; set; } = new List<PassportInfo>();
}
