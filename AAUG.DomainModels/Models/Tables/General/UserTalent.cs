using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class UserTalent
{
    public short Id { get; set; }

    public string Talent { get; set; } = null!;

    public int UserId { get; set; }

    // public virtual AaugUser? User { get; set; } = null!;
}
