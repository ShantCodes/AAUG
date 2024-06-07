using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels;
using AutoMapper;

namespace AAUG.DomainModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        NewsProfile();
    }

    private void NewsProfile()
    {
        CreateMap<News, NewsForInsertDto>();
        CreateMap<NewsForInsertDto, News>();
    }
}
