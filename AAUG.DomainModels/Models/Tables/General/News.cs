using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class News
{
    public int Id { get; set; }

    public string NewsTitle { get; set; } = null!;

    public string NewsDetails { get; set; } = null!;

    public int CreatorUserId { get; set; }
    public int NewsFileId { get; set; }

    public AaugUser AaugUser { get; set; }
    public MediaFile NewsFile { get; set; }
}
