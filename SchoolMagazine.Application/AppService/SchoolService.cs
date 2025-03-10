using AutoMapper;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppService.Paged;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
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

        public async Task<ServiceResponse<IEnumerable<SchoolDto>>> GetAllSchoolAsync()
        {

            // Retrieve all schools including all events and adverts 
            var schoolInfo = await _sr.GetAllSchoolAsync();

            var allSchools = _mapper.Map<IEnumerable<SchoolDto>>(schoolInfo);
            return new ServiceResponse<IEnumerable<SchoolDto>>(allSchools, success: true, message: "Schools were retrieved successfully!.");
        }


        public async Task<ServiceResponse<PagedResult<SchoolDto>>> GetPagedSchoolAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;


            var schools = await _sr.GetAllSchoolAsync();

            var totalCount = _sr.GetAllSchoolAsync().Result.Count();

            var result = new PagedResult<SchoolDto>
            {
                TotalCount = totalCount,
                Items = _mapper.Map<List<SchoolDto>>(schools)
            };

            return new ServiceResponse<PagedResult<SchoolDto>>(result, success: true, message: "Paged products retrieved successfully.");


        }

        public async Task<ServiceResponse<SchoolDto>> GetSchoolByIdAsync(Guid id)
        {
            var searchedSchool = await _sr.GetSchoolByIdAsync(id);
            if (searchedSchool == null)
            {
                return new ServiceResponse<SchoolDto>(null!, success: false, message: "School not found or not available.");

            }
            var schoolInfo = _mapper.Map<SchoolDto>(searchedSchool);

            return new ServiceResponse<SchoolDto>(schoolInfo, success: true, message: "School retrieved successfully.");
        }



        public async Task<ServiceResponse<SchoolDto>> GetSchoolByNameAsync(string schoolName)
        {
            // Retrieve the school from the repository
            var school = await _sr.GetSchoolByNameAsync(schoolName);

            // Check if the school exists
            if (school == null)
                return new ServiceResponse<SchoolDto>(null!, false, "School not found.");

            // Map the entity to DTO
            var schoolDto = _mapper.Map<SchoolDto>(school);

            return new ServiceResponse<SchoolDto>(schoolDto, true, "School retrieved successfully.");
        }

        public async Task<ServiceResponse<List<SchoolDto>>> GetSchoolsByLocationAsync(string location)
        {
            var schools = await _sr.GetSchoolsByLocationAsync(location);

            if (schools == null || !schools.Any())
                return new ServiceResponse<List<SchoolDto>>(null!, false, "No schools found in this location.");

            var schoolDtos = _mapper.Map<List<SchoolDto>>(schools);
            return new ServiceResponse<List<SchoolDto>>(schoolDtos, true, "Schools retrieved successfully.");
        }

        public async Task<ServiceResponse<List<SchoolDto>>> GetSchoolsByFeesRangeAsync(decimal feesRange)
        {
            var schools = await _sr.GetSchoolsByFeesRangeAsync(feesRange);

            if (schools == null || !schools.Any())
                return new ServiceResponse<List<SchoolDto>>(null!, false, "No schools found within this fees range.");

            var schoolDtos = _mapper.Map<List<SchoolDto>>(schools);
            return new ServiceResponse<List<SchoolDto>>(schoolDtos, true, "Schools retrieved successfully.");
        }


        public async Task<ServiceResponse<List<SchoolDto>>> GetSchoolsByRatingAsync(double rating)
        {
            var schools = await _sr.GetSchoolsByRatingAsync(rating);

            if (schools == null || !schools.Any())
                return new ServiceResponse<List<SchoolDto>>(null!, false, "No schools found with this rating.");

            var schoolDtos = _mapper.Map<List<SchoolDto>>(schools);
            return new ServiceResponse<List<SchoolDto>>(schoolDtos, true, "Schools retrieved successfully.");
        }




        public async Task<ServiceResponse<SchoolDto>> AddSchoolAsync(SchoolDto schoolDto)
        {
            // Check for duplicate before mapping
            var existingSchool = await _sr.GetSchoolByNameAsync(schoolDto.SchoolName);
            if (existingSchool != null)
                return new ServiceResponse<SchoolDto>(null!, false, "School name already exists.");

            var school = _mapper.Map<School>(schoolDto);

            // Attempt to save the entity
            var response = await _sr.AddSchoolAsync(school);
            if (!response.success)
                return new ServiceResponse<SchoolDto>(null!, false, response.message);

            // Map back to DTO
            var createdSchool = _mapper.Map<SchoolDto>(response.Data);

            return new ServiceResponse<SchoolDto>(createdSchool, true, "School was created successfully!");
        }





        public async Task<ServiceResponse<SchoolDto>> UpdateSchoolByIdAsync(Guid id, SchoolDto schoolDto)
        {
            var existingSchool = await _sr.GetSchoolByIdAsync(id);
            if (existingSchool == null)
                return new ServiceResponse<SchoolDto>(null!, false, "School not found.");

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
                var updatedSchoolDto = _mapper.Map<SchoolDto>(existingSchool);
                return new ServiceResponse<SchoolDto>(updatedSchoolDto, true, updateResult);
            }

            return new ServiceResponse<SchoolDto>(null!, false, updateResult);
        }

        public async Task<ServiceResponse<bool>> DeleteSchoolByIdAsync(Guid id)
        {
            var searchedSchool = await _sr.GetSchoolByIdAsync(id);

            if (searchedSchool == null)
            {
                return new ServiceResponse<bool>(false, success: false, message: "School was not found.");

            }

            // var school = _mapper.Map<School>(searchedSchool);
            //Task<ServiceResponse<bool>> DeleteProductByIdAsync(string id);



            await _sr.DeleteSchoolByIdAsync(searchedSchool);

            // var delSchool = _mapper.Map<SchoolDto>(searchedSchool);

            return new ServiceResponse<bool>(true, success: true, message: "School was removed successfully.");
        }





    }

}


