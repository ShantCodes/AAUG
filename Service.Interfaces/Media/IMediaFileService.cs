using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Media;
using Microsoft.AspNetCore.Http;

namespace AAUG.Service.Interfaces.Media;

public interface IMediaFileService
{
    Task<MediaFileInsertViewModel> InsertMediaFilesAsync(MediaFileInsertViewModel insertEntity);
    Task<MediaFileGetDto> InsertEventsMediaFileAsync(IFormFile file);
}
