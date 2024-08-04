using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Media;
using Microsoft.AspNetCore.Http;


namespace AAUG.Service.Interfaces.Media;

public interface IMediaFileService
{
    Task<MediaFileInsertViewModel> InsertMediaFilesAsync(MediaFileInsertViewModel insertEntity);
    Task<MediaFileGetDto> InsertEventsMediaFileAsync(IFormFile file);
    Task<MediaFileGetDto> InsertEventsMediaFileAsync(IFormFile file, int? existingFileId);
    Task<MediaFileGetDto> InsertUserMediaFileAsync(IFormFile file);
    Task<FileResultDto> DownloadMediaFileAsync(int fileId);
}
