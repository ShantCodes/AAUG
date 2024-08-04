using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class Camp
{
    public int Id { get; set; }

    public string CampingTitle { get; set; } = null!;

    public string? CampDetails { get; set; }

    public int RepresentatorUserId { get; set; }

    public DateOnly StartingDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool HasHappened { get; set; }

    public short UserMaxCapacity { get; set; }

    public virtual AaugUser? RepresentatorUser { get; set; } = null!;
}
