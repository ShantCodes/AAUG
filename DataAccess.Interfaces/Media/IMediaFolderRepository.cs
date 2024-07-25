using AAUG.DomainModels.Dtos.Media;

namespace AAUG.DataAccess.Interfaces.Media;

public interface IMediaFolderRepository
{
    IQueryable<MediaFolderPathDto> GetMediaFolderPath();
    IQueryable<MediaFolderPathDto> GetProfileFolder();
    IQueryable<MediaFolderPathDto> GetCampsFolder();
    IQueryable<MediaFolderPathDto> GetEventsFolder();
}