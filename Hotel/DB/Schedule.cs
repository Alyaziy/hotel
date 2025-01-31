using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class Schedule
{
    public int Id { get; set; }

    public int? StaffId { get; set; }

    public DateTime? Date { get; set; }

    public virtual Staff? Staff { get; set; }
}
