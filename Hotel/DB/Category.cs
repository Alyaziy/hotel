using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Cost { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
