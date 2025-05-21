using AutoMapper;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Entities.JobEntities;
using SchoolMagazine.Domain.Entities.VendorEntities;
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
            CreateMap<CreateProductDto, SchoolProduct>().ReverseMap();
            CreateMap<UpdateProductDto, SchoolProduct>().ReverseMap();
            CreateMap<SchoolProductDto, SchoolProduct>().ReverseMap();
            // Job Notifications
            CreateMap<JobPost, JobPostNotificationDto>().ReverseMap();

            CreateMap<JobNotificationSubscription, JobNotificationSubscriptionDto>()
      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Users.Email))
      .ReverseMap()
      .ForMember(dest => dest.Users, opt => opt.Ignore());



            CreateMap<VendorDto, SchoolVendor>().ReverseMap();
            CreateMap<CreateEventDto, SchoolAdvert>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();



        }
    }
}
