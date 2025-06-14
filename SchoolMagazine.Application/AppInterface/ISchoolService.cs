﻿using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppInterface
{
    public interface ISchoolService
    {

        Task<ServiceResponse<PagedResult<SchoolSummaryDto>>> GetSchoolsAsync(string? schoolName, string? address, decimal? feesRange, double? rating, int pageNumber, int pageSize);

        Task<ServiceResponse<PagedResult<PagedSchoolDto>>> GetPagedSchoolsAsync(int pageNumber, int pageSize);

        Task<ServiceResponse<CreateSchoolDto>> GetSchoolByIdAsync(Guid id);
        Task<ServiceResponse<CreateSchoolDto>> GetSchoolByNameAsync(string schoolName);

        Task<ServiceResponse<CreateSchoolDto>> AddSchoolAsync(CreateSchoolDto schoolDto);
        Task<ServiceResponse<CreateSchoolDto>> UpdateSchoolByIdAsync(Guid id, CreateSchoolDto schoolDto);
        Task<ServiceResponse<bool>> DeleteSchoolByIdAsync(Guid id);




    }



}
