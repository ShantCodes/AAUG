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
        }

        private void NewsProfile()
        {
            CreateMap<NewsForInsertDto, NewsForInsertViewModel>();
            CreateMap<NewsForInsertDto, NewsForShowViewModel>();
            CreateMap<NewsForInsertViewModel, NewsForInsertDto>();
            CreateMap<News, NewsForShowViewModel>();
            CreateMap<NewsForInsertViewModel, News>();
            CreateMap<NewsForEditViewModel, News>();
        }
    }
}
