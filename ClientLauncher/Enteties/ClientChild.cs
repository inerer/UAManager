using System;
using System.Collections.Generic;

namespace ClientLauncher.Enteties;

public partial class ClientChild
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int? ClientInfoId { get; set; }

    public virtual ClientInfo? ClientInfo { get; set; }
}
