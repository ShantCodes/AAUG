using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAUG.DomainModels;
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
        }

        private void NewsProfile()
        {
            CreateMap<NewsForInsertDto, NewsForInsertViewModel>();
            CreateMap<NewsForInsertViewModel, NewsForInsertDto>();
        }
    }
}
