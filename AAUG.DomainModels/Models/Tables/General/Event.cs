using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class Event
{
    public int Id { get; set; }

    public string EventTitle { get; set; } = null!;

    public string? EventDetails { get; set; }

    public DateOnly EventDate { get; set; }

    public string? Presentator { get; set; }

    public int? PresentatorUserId { get; set; }

    public int? ThumbNailFileId { get; set; }

    public bool IsApproved { get; set; }

    public bool HasHappened { get; set; }

    public virtual ICollection<EventLike> EventLikes { get; set; } = new List<EventLike>();

    public virtual AaugUser? PresentatorUser { get; set; }

    public virtual MediaFile? ThumbnailFile { get; set; }
}
