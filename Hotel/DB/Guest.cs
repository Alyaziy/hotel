using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DB;

public partial class Guest
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual User? User { get; set; }

    [NotMapped]
    public string FIO => LastName + " " + FirstName + " " + MiddleName;
}
