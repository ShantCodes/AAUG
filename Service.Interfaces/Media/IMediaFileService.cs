using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Media;
using Microsoft.AspNetCore.Http;


namespace AAUG.Service.Interfaces.Media;

public interface IMediaFileService
{
    Task<MediaFileInsertViewModel> InsertMediaFilesAsync(MediaFileInsertViewModel insertEntity);
    #region user
    Task<MediaFileGetDto> InsertUserMediaFileAsync(IFormFile file);
    Task<MediaFileGetDto> InsertUserMediaFileAsync(IFormFile file, int? existingFileId);
    #endregion

    #region events
    Task<MediaFileGetDto> InsertEventsMediaFileAsync(IFormFile file);
    Task<MediaFileGetDto> InsertEventsMediaFileAsync(IFormFile file, int? existingFileId);
    #endregion

    #region news
    Task<MediaFileGetDto> InsertNewsMediaFileAsync(IFormFile file);
    Task<MediaFileGetDto> InsertNewsMediaFileAsync(IFormFile file, int? existingFileId);
    #endregion
    
    #region download
    Task<FileResultDto> DownloadMediaFileAsync(int fileId);
    #endregion
}
