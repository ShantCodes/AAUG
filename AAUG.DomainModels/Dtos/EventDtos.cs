using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.ViewModels;

namespace AAUG.DomainModels.Dtos;

public class EventInsertDto
{
    public string EventTitle { get; set; } = null!;

    public string? EventDetails { get; set; }

    public DateOnly EventDate { get; set; }

    public string? Presentator { get; set; }

    public int? PresentatorUserId { get; set; }

    public int? ThumbNailFileId { get; set; }

    public bool IsApproved { get; set; }

    public bool HasHappened { get; set; }

}

public class EventEditDto
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

}

public class EventGetDto
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

    public MediaFileGetDto? ThumbnailFile { get; set; }
}
