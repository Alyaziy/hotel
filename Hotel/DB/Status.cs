﻿using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class Status
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
