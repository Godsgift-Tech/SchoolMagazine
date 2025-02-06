using SchoolMagazine.Application.AppService.Paged;
using SchoolMagazine.Application.AppService.Service_Response_Model;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Application.Queries;
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
        Task<ServiceResponse<IEnumerable<SchoolDto>>> GetAllSchoolAsync();
        Task<ServiceResponse<PagedResult<SchoolDto>>> GetPagedSchoolAsync(int pageNumber, int pageSize);


        Task<ServiceResponse<SchoolDto>> GetSchoolByIdAsync(Guid id);
        //  Task<ServiceResponse<SchoolDto>> RateSchoolByIdAsync(Guid schoolId, double rating);
        Task<ServiceResponse<SchoolDto>> AddSchoolAsync(SchoolDto schoolDto);
        Task<ServiceResponse<SchoolDto>> UpdateSchoolByIdAsync(Guid id, SchoolDto schoolDto);
        Task<ServiceResponse<bool>> DeleteSchoolByIdAsync(Guid id, SchoolDto schoolDto);

        // Task <ServiceResponse<IEnumerable<SchoolDto>>>SchoolQueryAsync(SchoolSearchQuery schoolSearchQuery);



    }



}
