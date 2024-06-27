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
        }

        private void AaugUserProfile()
        {
            CreateMap<AaugUserInsertViewModel, AaugUsersInsertDto>();
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
