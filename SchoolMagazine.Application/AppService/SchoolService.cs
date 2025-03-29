using AutoMapper;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppService
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _sr;
        private readonly IMapper _mapper;

        public SchoolService(ISchoolRepository sr, IMapper mapper)
        {
            _sr = sr;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<PagedResult<SchoolDto>>> GetPagedSchoolsAsync(int pageNumber, int pageSize)
        {
            var pagedResult = await _sr.GetPagedResultAsync(pageNumber, pageSize);

            var pagedResultDto = new PagedResult<SchoolDto>
            {
                TotalCount = pagedResult.TotalCount,
                PageSize = pagedResult.PageSize,
                PageNumber = pagedResult.PageNumber,
                Items = _mapper.Map<List<SchoolDto>>(pagedResult.Items)
            };

            return new ServiceResponse<PagedResult<SchoolDto>>(pagedResultDto, success: true, message: "Schools with advert and event details retrieved successfully");
        }

        //

        //public async Task<ServiceResponse<PagedResult<SchoolDto>>> GetEventsPagedSchoolsAsync(int pageNumber, int pageSize)
        //{
        //    var pagedResult = await _sr.GetEventPagedResultAsync(pageNumber, pageSize);

        //    var pagedResultDto = new PagedResult<SchoolDto>
        //    {
        //        TotalCount = pagedResult.TotalCount,
        //        PageSize = pagedResult.PageSize,
        //        PageNumber = pagedResult.PageNumber,
        //        Items = _mapper.Map<List<SchoolDto>>(pagedResult.Items)
        //    };

        //    return new ServiceResponse<PagedResult<SchoolDto>>(pagedResultDto, success: true, message: "Schools with event details retrieved successfully");
        //}
        public async Task<ServiceResponse<CreateSchoolDto>> GetSchoolByIdAsync(Guid id)
        {
            var searchedSchool = await _sr.GetSchoolByIdAsync(id);
            if (searchedSchool == null)
            {
                return new ServiceResponse<CreateSchoolDto>(null!, success: false, message: "School not found or not available.");

            }
            var schoolInfo = _mapper.Map<CreateSchoolDto>(searchedSchool);

            return new ServiceResponse<CreateSchoolDto>(schoolInfo, success: true, message: "School retrieved successfully.");
        }



        public async Task<ServiceResponse<CreateSchoolDto>> GetSchoolByNameAsync(string schoolName)
        {
            // Retrieve the school from the repository
            var school = await _sr.GetSchoolByNameAsync(schoolName);

            // Check if the school exists
            if (school == null)
                return new ServiceResponse<CreateSchoolDto>(null!, false, "School not found.");

            // Map the entity to DTO
            var schoolDto = _mapper.Map<CreateSchoolDto>(school);

            return new ServiceResponse<CreateSchoolDto>(schoolDto, true, "School retrieved successfully.");
        }




        public async Task<ServiceResponse<CreateSchoolDto>> AddSchoolAsync(CreateSchoolDto schoolDto)
        {
            // Check for duplicate before mapping
            var existingSchool = await _sr.GetSchoolByNameAsync(schoolDto.SchoolName);
            if (existingSchool != null)
                return new ServiceResponse<CreateSchoolDto>(null!, false, "School name already exists.");

            var school = _mapper.Map<School>(schoolDto);

            // Attempt to save the entity
            var response = await _sr.AddSchoolAsync(school);
            if (!response.success)
                return new ServiceResponse<CreateSchoolDto>(null!, false, response.message);

            // Map back to DTO
            var createdSchool = _mapper.Map<CreateSchoolDto>(response.Data);

            return new ServiceResponse<CreateSchoolDto>(createdSchool, true, "School was created successfully!");
        }



        public async Task<ServiceResponse<CreateSchoolDto>> UpdateSchoolByIdAsync(Guid id, CreateSchoolDto schoolDto)
        {
            var existingSchool = await _sr.GetSchoolByIdAsync(id);
            if (existingSchool == null)
                return new ServiceResponse<CreateSchoolDto>(null!, false, "School not found.");

            // Update school properties
            existingSchool.SchoolName = schoolDto.SchoolName;
            existingSchool.Location = schoolDto.Location;
            existingSchool.EmailAddress = schoolDto.EmailAddress;
            existingSchool.PhoneNumber = schoolDto.PhoneNumber;
            existingSchool.WebsiteUrl = schoolDto.WebsiteUrl;
            existingSchool.FeesRange = schoolDto.FeesRange;
            existingSchool.Rating = schoolDto.Rating;

            // Get the string result from repository
            var updateResult = await _sr.UpdateSchoolAsync(existingSchool);

            if (updateResult == "School updated successfully.")
            {
                var updatedSchoolDto = _mapper.Map<CreateSchoolDto>(existingSchool);
                return new ServiceResponse<CreateSchoolDto>(updatedSchoolDto, true, updateResult);
            }

            return new ServiceResponse<CreateSchoolDto>(null!, false, updateResult);
        }

        public async Task<ServiceResponse<bool>> DeleteSchoolByIdAsync(Guid id)
        {
            var searchedSchool = await _sr.GetSchoolByIdAsync(id);

            if (searchedSchool == null)
            {
                return new ServiceResponse<bool>(false, success: false, message: "School was not found.");

            }

            



            await _sr.DeleteSchoolByIdAsync(searchedSchool);


            return new ServiceResponse<bool>(true, success: true, message: "School was removed successfully.");
        }

        public async Task<ServiceResponse<PagedResult<SchoolDto>>> GetSchoolsAsync(
    string? schoolName, string? location, decimal? feesRange, double? rating, int pageNumber, int pageSize)
        {
            var pagedSchools = await _sr.GetSchoolsAsync(schoolName, location, feesRange, rating, pageNumber, pageSize);

            if (pagedSchools.Items.Count == 0)
                return new ServiceResponse<PagedResult<SchoolDto>>(null!, false, "No schools found matching criteria.");

            var schoolDtos = _mapper.Map<List<SchoolDto>>(pagedSchools.Items);

            var result = new PagedResult<SchoolDto>
            {
                TotalCount = pagedSchools.TotalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = schoolDtos
            };

            return new ServiceResponse<PagedResult<SchoolDto>>(result, true, "Schools retrieved successfully.");
        }




    }

}


