using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class Room
{
    public int Id { get; set; }

    public int? Floor { get; set; }

    public int? Number { get; set; }

    public int? CategoryId { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual Category? Category { get; set; }

    public virtual Status? Status { get; set; }
}
