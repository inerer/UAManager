using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class PassportInfo
{
    public int Id { get; set; }

    public string Series { get; set; } = null!;

    public string Number { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public int? CitizenshipId { get; set; }

    public virtual Citizenship? Citizenship { get; set; }

    public virtual ICollection<ClientInfo> ClientInfos { get; set; } = new List<ClientInfo>();
}
