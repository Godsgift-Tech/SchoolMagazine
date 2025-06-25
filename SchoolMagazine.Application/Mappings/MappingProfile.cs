using AutoMapper;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Application.DTOs.Dashboard;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Entities.JobEntities;
using SchoolMagazine.Domain.Entities.VendorEntities;
using SchoolMagazine.Domain.UserRoleInfo;

namespace SchoolMagazine.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {

            CreateMap<School, PagedSchoolDto>().ReverseMap();
            CreateMap<School, CreateSchoolDto>().ReverseMap();

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

          

            //
            CreateMap<SchoolRating, SchoolRatingDto>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
               .ForMember(dest => dest.RatingValue, opt => opt.MapFrom(src => src.RatingValue))
               .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
               .ForMember(dest => dest.RatedAt, opt => opt.MapFrom(src => src.RatedAt));

            CreateMap<School, SchoolWithRatingDto>()
                .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.SchoolName))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Ratings.Any() ? src.Ratings.Average(r => r.RatingValue) : 0));


          

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

            //

            CreateMap<JobApplication, JobApplicationDto>()
    .ForMember(dest => dest.ApplicantFullName,
        opt => opt.MapFrom(src => src.Users.FirstName + " " + src.Users.LastName))
    .ForMember(dest => dest.ApplicantEmail,
        opt => opt.MapFrom(src => src.Users.Email))
    .ForMember(dest => dest.JobTitle,
        opt => opt.MapFrom(src => src.JobPosting.Title));

            //

            CreateMap<JobPost, JobPostDto>()
    .ForMember(dest => dest.PostedByFullName,
        opt => opt.MapFrom(src => src.PostedBy.FirstName + " " + src.PostedBy.LastName))
    .ForMember(dest => dest.SchoolName,
        opt => opt.MapFrom(src => src.School.SchoolName))
    .ForMember(dest => dest.NumberOfApplications,
        opt => opt.MapFrom(src => src.Applications.Count));


        }
    }
}
