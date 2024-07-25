using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Dtos.Media;
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

        MediaFolderProfile();
    }

    private void MediaFolderProfile()
    {
        CreateMap<MediaFolder, MediaFolderPathDto>()
        .ForMember(a => a.MediaDriveId, a => a.Ignore());

        CreateMap<MediaFileInsertDto, MediaFile>();
        CreateMap<MediaFileInsertDto, MediaFileGetDto>();
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
