using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAUG.DomainModels;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels;
using AutoMapper;

namespace AAUG.Service.Implementations.ViewModelMapper
{
    public class ViewModelAutoMapper : Profile
    {
        public ViewModelAutoMapper()
        {
            NewsProfile();

            AaugUserProfile();

            EventProfile();

            EventLikeProfile();

            SlideShowProfile();
        }

        private void SlideShowProfile()
        {
            CreateMap<SlideShowGetDto, SlideShowGetViewModel>();
        }

        private void EventLikeProfile()
        {
            CreateMap<EventLikeGetDto, EventLikeGetViewModel>()
            .ForMember(a => a.User, a => a.Ignore());
            CreateMap<EventLikeGetDto, EventLikeDeleteDto>();
        }

        private void EventProfile()
        {
            CreateMap<Event, EventGetViewModel>();
            CreateMap<EventGetDto, EventWithMediaGetViewModel>()
                .ForMember(a => a.ThumbnailBase64, a => a.Ignore());
            CreateMap<EventEditViewModel, EventGetDto>()
            .ForMember(a => a.ThumbNailFileId, a => a.Ignore());

            CreateMap<EventEditViewModel, EventEditDto>()
            .ForMember(a => a.ThumbNailFileId, a => a.Ignore());
        }

        private void AaugUserProfile()
        {
            CreateMap<AaugUserInsertViewModel, AaugUsersInsertDto>();
            CreateMap<RegisterDto, AaugUserInsertViewModel>();
            CreateMap<AaugUser, AaugUserGetViewModel>();
            CreateMap<AaugUserFullEditViewModel, AaugUsersEditDto>()
            .ForMember(a => a.NationalCardFileId, a => a.Ignore())
            .ForMember(a => a.ProfilePictureFileId, a => a.Ignore())
            .ForMember(a => a.UniversityCardFileId, a => a.Ignore());

            CreateMap<AaugUserGetDto, AaugUserGetViewModel>();
            CreateMap<AaugUserFullGetDto, AaugUserFullInsertViewModel>();
            CreateMap<AaugUser, AaugUserFullGetViewModel>();
            CreateMap<AaugUser, AaugUserWithProfilePicureGetViewModel>();

            CreateMap<AaugUserWithProfilePictureGetDto, AaugUserWithProfilePicureGetViewModel>();

            CreateMap<AaugUserFullGetDto, AaugUserFullGetViewModel>();
        }

        private void NewsProfile()
        {
            CreateMap<NewsForInsertDto, NewsForInsertViewModel>();
            CreateMap<NewsForInsertDto, NewsForShowViewModel>();
            CreateMap<NewsForInsertViewModel, NewsForInsertDto>()
            .ForMember(a => a.NewsFileId, a => a.Ignore());
            CreateMap<News, NewsForShowViewModel>();
            CreateMap<NewsForInsertViewModel, News>();
            CreateMap<NewsForEditViewModel, News>();
            CreateMap<News, NewsForShowViewModel>();
            CreateMap<NewsForEditViewModel, NewsForEditDto>()
            .ForMember(a => a.NewsFileId, a => a.Ignore());

            CreateMap<NewsGetDto, NewsForShowViewModel>();
        }
    }
}
