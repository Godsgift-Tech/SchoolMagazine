using SchoolMagazine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Interface
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<School>> GetAllSchoolAsync();
        Task<School> GetSchoolByIdAsync(Guid id);
        Task AddSchoolAsync(School school);
        Task UpdateSchoolByIdAsync(School school);
        Task DeleteSchoolByIdAsync(School searchedSchool);
    }
}
