using Domain.Models;
using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Profile
{
    public Guid Id { get; set; }= Guid.NewGuid();

    public byte[] ProfileName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Discription { get; set; } = null!;

    public byte[] Image { get; set; } = null!;

    public Guid User { get; set; }

    public virtual User UserNavigation { get; set; } = null!;
}
