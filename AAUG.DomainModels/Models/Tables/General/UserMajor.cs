using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class UserMajor
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Major { get; set; } = null!;

    public bool HasGraduated { get; set; }

    public DateOnly? GraduationDate { get; set; }

    public DateOnly EntryDate { get; set; }

    public virtual AaugUser User { get; set; } = null!;
}
