using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels;
using AutoMapper;

namespace AAUG.DomainModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        NewsProfile();

        AaugUserProfile();

        EventProfile();
    }

    private void EventProfile()
    {
        CreateMap<EventInsertViewModel, EventInsertDto>();
        CreateMap<EventInsertDto, Event>();
        CreateMap<EventGetDto, EventGetViewModel>();
    }

    private void AaugUserProfile()
    {
        CreateMap<AaugUsersInsertDto, AaugUser>();
        CreateMap<AaugUser, EventGetDto>();
    }

    private void NewsProfile()
    {
        CreateMap<News, NewsForInsertDto>();
        CreateMap<NewsForInsertDto, News>();
    }
}
