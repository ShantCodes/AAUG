using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface ISlideShowTitleRepository
{
    Task<SlideShowTitle> AddSlideShowTitle(SlideShowTitleInsertDto inputEntity);
    Task<SlideShowTitle> GetDataAsync();
    Task<bool> DeleteAsync(int id);
}