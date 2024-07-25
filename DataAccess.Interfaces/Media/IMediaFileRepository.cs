﻿using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.Media;

public interface IMediaFileRepository
{
    Task<MediaFile> AddMediaFileAsync(MediaFileInsertDto insertEntity);
}
