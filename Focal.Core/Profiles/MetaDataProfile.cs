using AutoMapper;
using Focal.Core.DTOS;
using Focal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focal.Core.Profiles
{
    public class MetaDataProfile : Profile
    {
        public MetaDataProfile()
        {
            CreateMap<MetaData, MetaDataDTO>()
                .ReverseMap();
        }
    }
}
