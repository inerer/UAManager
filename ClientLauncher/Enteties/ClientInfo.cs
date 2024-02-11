using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class ClientInfo
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int? PassportInfoId { get; set; }

    public bool IsDisabled { get; set; }

    public bool IsChild { get; set; }

    public virtual ICollection<ClientChild> ClientChildren { get; set; } = new List<ClientChild>();

    public virtual PassportInfo? PassportInfo { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
