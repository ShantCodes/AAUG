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

        MediaFIleProfile();

        EventLikeProfile();
    }

    private void EventLikeProfile()
    {
        CreateMap<EventLike, EventLikeGetDto>();
        CreateMap<EventLikeInsertDto, EventLike>();
        CreateMap<EventLikeDeleteDto, EventLike>();
    }

    private void MediaFIleProfile()
    {
        CreateMap<MediaFile, MediaFileGetDto>();
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
        CreateMap<Event, EventGetDto>();
        // .ForMember(a => a.ThumbnailFile, a => a.Ignore());
        CreateMap<MediaFile, MediaFolderPathDto>();
        CreateMap<EventEditViewModel, EventGetDto>()
        .ForMember(a => a.ThumbnailFile, a => a.Ignore());

        CreateMap<EventEditDto, Event>();

    }

    private void AaugUserProfile()
    {
        CreateMap<AaugUsersInsertDto, AaugUser>();
        CreateMap<AaugUser, EventGetDto>();
        CreateMap<AaugUser, AaugUserFullGetDto>();
        CreateMap<AaugUser, AaugUserGetDto>();
        CreateMap<AaugUsersEditDto, AaugUser>();
        CreateMap<AaugUser, AaugUserWithProfilePictureGetDto>();
    }

    private void NewsProfile()
    {
        CreateMap<News, NewsForInsertDto>();
        CreateMap<NewsForInsertDto, News>();
        CreateMap<News, NewsGetDto>();
    }
}
