using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _sr;

        public SchoolService(ISchoolRepository sr)
        {
            _sr = sr;
        }

        public async Task AddSchoolAsync(SchoolDto schoolDto)
        {
            var school = new School
            {
                Adverts = schoolDto.Adverts,
                Events = schoolDto.Events,
                FeesRange = schoolDto.FeesRange,
                //Id = schoolDto.Id,
                Location = schoolDto.Location,
                Name = schoolDto.Name,
                Rating = schoolDto.Rating,
                WebsiteUrl = schoolDto.WebsiteUrl,

            };
            await _sr.AddSchoolAsync(school);

        }

        public async Task<IEnumerable<School>> GetAllSchoolAsync()
        {
            return await _sr.GetAllSchoolAsync();   
        }

        public async Task<School> GetSchoolByIdAsync(Guid id)
        {
            return await _sr.GetSchoolByIdAsync(id);
        }
    }
}
