using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.Media;

public interface IMediaFileRepository
{
    Task<MediaFile> AddMediaFileAsync(MediaFileInsertDto insertEntity);
    IQueryable<MediaFileGetDto> GetMediaFile(int fileId);
    IQueryable<MediaFileGetDto> GetMediaFileByGuIdAsync(int id);
}
