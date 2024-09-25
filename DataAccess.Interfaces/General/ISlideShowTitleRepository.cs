using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface ISlideShowTitleRepository
{
    Task<SlideShowTitle> AddSlideShowTitle(SlideShowTitleInsertDto inputEntity);
    IQueryable<SlideshowTitleGetDto> GetData();
    Task<bool> DeleteAsync(int id);
}