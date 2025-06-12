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

            CreateMap<School, PagedSchoolDto>().ReverseMap();
           // CreateMap<School, CreateSchoolDto>().ReverseMap();

            CreateMap<CreateSchoolDto, School>()
           .ForMember(dest => dest.Images, opt => opt.MapFrom(src =>
               src.ImageUrls != null
               ? src.ImageUrls.Select(url => new SchoolImage { ImageUrl = url }).ToList()
               : new List<SchoolImage>()
           ));

            //
            CreateMap<School, SchoolDto>()
            .ForMember(dest => dest.ImageUrls,
                opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl).ToList()));

            CreateMap<School, SchoolSummaryDto>().ReverseMap();
            CreateMap<SchoolEvent, SchoolEventDto>().ReverseMap();
            CreateMap<ApprovedVendorDto, SchoolVendor>().ReverseMap();
           // CreateMap<SchoolAdvert, SchoolAdvertDto>().ReverseMap();

            CreateMap<SchoolAdvert, SchoolAdvertDto>().ReverseMap();
            // ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.SchoolName));

            CreateMap<CreateAdvertDto, SchoolAdvert>().ReverseMap();
            CreateMap<CreateProductDto, SchoolProduct>().ReverseMap();
            CreateMap<UpdateProductDto, SchoolProduct>().ReverseMap();
            CreateMap<SchoolProductDto, SchoolProduct>().ReverseMap();
            // Job Notifications
            CreateMap<JobPost, JobPostNotificationDto>().ReverseMap();

      //      CreateMap<JobNotificationSubscription, JobNotificationSubscriptionDto>()
      //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Users.Email))
      //.ReverseMap()
      //.ForMember(dest => dest.Users, opt => opt.Ignore());

            //
            CreateMap<JobNotificationSubscriptionDto, JobNotificationSubscription>()
           .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
               src.Categories.Select(c => new JobCategoryPreference { CategoryName = c }).ToList()));

            CreateMap<JobNotificationSubscription, JobNotificationSubscriptionDto>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
                    src.Categories.Select(c => c.CategoryName).ToList()));



            //

            CreateMap<VendorDto, SchoolVendor>().ReverseMap();
            CreateMap<CreateEventDto, SchoolAdvert>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();



        }
    }
}
