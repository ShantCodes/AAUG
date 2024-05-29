using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class News
{
    public int Id { get; set; }

    public string NewsTitle { get; set; } = null!;

    public string NewsDetails { get; set; } = null!;
}
