using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class Book
{
    public int Id { get; set; }

    public int? GuestId { get; set; }

    public int? RoomId { get; set; }

    public DateTime? DateIn { get; set; }

    public DateTime? DateOut { get; set; }

    public virtual ICollection<BookService> BookServices { get; set; } = new List<BookService>();

    public virtual Guest? Guest { get; set; }

    public virtual Room? Room { get; set; }
}
