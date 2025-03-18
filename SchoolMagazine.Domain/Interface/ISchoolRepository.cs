using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;
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
        //Task AddSchoolAsync(School school);
        //Task<bool> AddSchoolAsync(School school);
        Task<School?> GetSchoolByNameAsync(string schoolName);
        Task<List<School>> GetSchoolsByLocationAsync(string location);
        Task<List<School>> GetSchoolsByFeesRangeAsync(decimal feesRange);
        Task<List<School>> GetSchoolsByRatingAsync(double rating);

        Task<ServiceResponse<School>> AddSchoolAsync(School school);
        // Task<PagedResult<School>> GetPagedSchoolAsync(int pageNumber, int pageSize);
        Task<PagedResult<School>> GetPagedResultAsync(int pageNumber, int pageSize);

        Task<string> UpdateSchoolAsync(School school);
      //  Task UpdateSchoolByIdAsync(School school);
        Task DeleteSchoolByIdAsync(School searchedSchool);
    }
}
