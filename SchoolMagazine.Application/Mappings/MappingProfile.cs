using AutoMapper;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {

            CreateMap<School, SchoolDto>().ReverseMap();
            CreateMap<SchoolEvent, SchoolEventDto>().ReverseMap();
            CreateMap<SchoolAdvert, SchoolAdvertDto>().ReverseMap();



        }
    }
}
