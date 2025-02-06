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

            //// Map School to SchoolDto
            //CreateMap<School, SchoolDto>()
            //    //->.ForMember(dest => dest.AdminName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            //    .ForMember(dest => dest.Events, opt => opt.MapFrom(src => src.Events))
            //  //  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Adverts, opt => opt.MapFrom(src => src.Adverts));

            //// Map SchoolDto to School (if required)
            //CreateMap<SchoolDto, School>()
            //   //-> .ForMember(dest => dest.User, opt => opt.Ignore()) // Avoid nested mapping here
            //    .ForMember(dest => dest.Events, opt => opt.Ignore()) // Avoid nested mapping
            //    .ForMember(dest => dest.Adverts, opt => opt.Ignore());


            ////



            //// Map SchoolEvent to SchoolEventDto
            //CreateMap<SchoolEvent, SchoolEventDto>()
            //    .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.SchoolId)) // Map SchoolId
            //    .ForMember(dest => dest.SchoolName, opt => opt.Ignore()); // Ignore the navigation property

            //// Map SchoolEventDto to SchoolEvent
            //CreateMap<SchoolEventDto, SchoolEvent>()
            //    .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.SchoolId)) // Map SchoolId
            //    .ForMember(dest => dest.SchoolName, opt => opt.Ignore()); // Ignore the navigation property



            //// Map SchoolAdvert to SchoolAdvertDto
            //CreateMap<SchoolAdvert, SchoolAdvertDto>()
            //    .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.SchoolId)) // Map SchoolId
            //    .ForMember(dest => dest.SchoolName, opt => opt.Ignore()); // Ignore the navigation property

            //// Map SchoolAdvertDto to SchoolAdvert
            //CreateMap<SchoolAdvertDto, SchoolAdvert>()
            //    .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.SchoolId)) // Map SchoolId
            //    .ForMember(dest => dest.SchoolName, opt => opt.Ignore()); // Ignore the navigation property



        }
    }
}
