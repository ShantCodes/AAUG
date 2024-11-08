﻿using AAUG.DomainModels.Dtos.Media;
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
