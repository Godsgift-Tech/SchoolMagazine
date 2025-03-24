using AutoMapper;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.UserRoleInfo;
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
            CreateMap<School, CreateSchoolDto>().ReverseMap();
            CreateMap<SchoolEvent, SchoolEventDto>().ReverseMap();
           // CreateMap<SchoolAdvert, SchoolAdvertDto>().ReverseMap();

            CreateMap<SchoolAdvert, SchoolAdvertDto>().ReverseMap();
            // ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.SchoolName));

            CreateMap<CreateAdvertDto, SchoolAdvert>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();



        }
    }
}
