using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public bool IsLocked { get; set; }

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

    public virtual Role? Role { get; set; }
}
