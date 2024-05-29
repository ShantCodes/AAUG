using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class EventLike
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual AaugUser User { get; set; } = null!;
}
