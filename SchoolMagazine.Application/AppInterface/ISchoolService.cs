using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppInterface
{
    public interface ISchoolService
    {
        Task<IEnumerable<School>> GetAllSchoolAsync();
        Task<School> GetSchoolByIdAsync(Guid id);
        Task AddSchoolAsync(SchoolDto schoolDto);
    }
}
