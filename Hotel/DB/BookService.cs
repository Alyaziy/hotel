using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class BookService
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public int? ServiceId { get; set; }

    public DateTime? Date { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Service? Service { get; set; }
}
