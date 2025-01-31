using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class Service
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Cost { get; set; }

    public virtual ICollection<BookService> BookServices { get; set; } = new List<BookService>();

    public virtual ICollection<ServiceStaff> ServiceStaffs { get; set; } = new List<ServiceStaff>();
}
