using AAUG.DomainModels.Dtos.Media;
using Microsoft.AspNetCore.Http;

namespace AAUG.DomainModels.ViewModels;

public class EventGetViewModel
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
    public short LikeCount { get; set; }


    public AaugUserGetViewModel? AaugUserGetViewModel { get; set; }

    public MediaFileGetDto? thumbnailFile { get; set; }

}

public class EventWithMediaGetViewModel
{
    public int Id { get; set; }

    public string EventTitle { get; set; } = null!;

    public string? EventDetails { get; set; }

    public DateOnly EventDate { get; set; }

    public string? Presentator { get; set; }

    public int? PresentatorUserId { get; set; }

    public int? ThumbNailFileId { get; set; }
    public string? ThumbnailBase64 { get; set; }

    public bool IsApproved { get; set; }

    public bool HasHappened { get; set; }

    public AaugUserGetViewModel? AaugUserGetViewModel { get; set; }

    public MediaFileGetDto? thumbnailFile { get; set; }
}

public class EventInsertViewModel
{
    public string? EventTitle { get; set; }

    public string? EventDetails { get; set; }

    public DateOnly EventDate { get; set; }

    public string? Presentator { get; set; }

    public int? PresentatorUserId { get; set; }

    public IFormFile? ThumbNailFile { get; set; }
}

public class EventEditViewModel
{
    public int Id { get; set; }
    public string? EventTitle { get; set; }

    public string? EventDetails { get; set; }

    public DateOnly EventDate { get; set; }

    public string? Presentator { get; set; }

    public IFormFile? ThumbNailFile { get; set; }
}

#region event details
public class EventDetailsGetViewModel
{
    public int Id { get; set; }
    public string Details { get; set; }
    public byte OrderBy { get; set; }
    public int EventId { get; set; }
    public List<EventDetailsFileGetViewModel> EventDetailsFileGetViewModels { get; set; }

}

public class EventDetailsFileGetViewModel
{
    public int Id { get; set; }
    public int MediaFileId { get; set; }
    public int ExpandEventTextId { get; set; }
}

public class EventDetailsTextInsertViewModel
{
    public string Details { get; set; }
    public byte OrderBy { get; set; }
    public int EventId { get; set; }
}
public class EventDetailsTextEditViewModel
{
    public int Id { get; set; }
    public string Details { get; set; }
    public byte? OrderBy { get; set; }
}
public class EventDetailsTextGetViewModel
{
    public string Details { get; set; }
    public byte OrderBy { get; set; }
    public int EventId { get; set; }
}

public class EventDetailsFileInsertViewModel
{
    public int ExpandEventTextId { get; set; }
    public IFormFile EventDetailFile { get; set; }
}
#endregion
