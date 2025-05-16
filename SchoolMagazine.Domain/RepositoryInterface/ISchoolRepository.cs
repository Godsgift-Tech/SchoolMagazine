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
        Task<PagedResult<School>> GetSchoolsAsync(
        string? schoolName,
        string? location,
        decimal? feesRange,
        double? rating,
        int pageNumber,
        int pageSize);
        Task<School> GetSchoolByIdAsync(Guid id);
       
        Task<School?> GetSchoolByNameAsync(string schoolName);
      
        Task<ServiceResponse<School>> AddSchoolAsync(School school);
        Task<PagedResult<School>> GetPagedResultAsync(int pageNumber, int pageSize);
        //Task<PagedResult<School>> GetEventPagedResultAsync(int pageNumber, int pageSize);

        Task<string> UpdateSchoolAsync(School school);
        Task DeleteSchoolByIdAsync(School searchedSchool);
    }
}
