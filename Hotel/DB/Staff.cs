using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class Staff
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<ServiceStaff> ServiceStaffs { get; set; } = new List<ServiceStaff>();
}
