using System;
using System.Collections.Generic;

namespace Hotel.DB;

public partial class ServiceStaff
{
    public int Id { get; set; }

    public int? ServiceId { get; set; }

    public int? StaffId { get; set; }

    public virtual Service? Service { get; set; }

    public virtual Staff? Staff { get; set; }
}
