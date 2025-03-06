using AutoMapper;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.AppService.Paged;
using SchoolMagazine.Application.AppService.Service_Response_Model;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Application.Queries;
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


        public async Task<ServiceResponse<SchoolDto>> AddSchoolAsync(SchoolDto schoolDto)
        {
            try
            {
                var school = _mapper.Map<School>(schoolDto);

                // Attempt to save the entity
                await _sr.AddSchoolAsync(school);

                // Map back to DTO
                var createdSchool = _mapper.Map<SchoolDto>(school);

                return new ServiceResponse<SchoolDto>(createdSchool, success: true, message: "School was created successfully!");
            }
            //catch (DbUpdateException dbEx)
            //{
            //    // Log the entire error stack for debugging
            //    Console.WriteLine($"DbUpdateException: {dbEx}");

            //    if (dbEx.InnerException != null)
            //    {
            //        Console.WriteLine($"Inner Exception: {dbEx.InnerException}");

            //        // Try casting the inner exception to SqlException
            //        var sqlEx = dbEx.InnerException as SqlException;
            //        if (sqlEx != null)
            //        {
            //            Console.WriteLine($"SQL Error Code: {sqlEx.Number}");

            //            switch (sqlEx.Number)
            //            {
            //                case 2627: // Primary key violation
            //                    return new ServiceResponse<SchoolDto>(null, success: false, message: "A school with this ID already exists.");

            //                case 2601: // Unique constraint violation
            //                    return new ServiceResponse<SchoolDto>(null, success: false, message: "A school with this name already exists.");

            //                default:
            //                    return new ServiceResponse<SchoolDto>(null, success: false, message: $"Database error (Code {sqlEx.Number}): {sqlEx.Message}");
            //            }
            //        }
            //    }

            //    return new ServiceResponse<SchoolDto>(null, success: false, message: $"Database error: {dbEx.Message}");

            //}
            catch (Exception ex)
            {
                // Log full details of the error
                Console.WriteLine($"Exception: {ex}");
                return new ServiceResponse<SchoolDto>(null, success: false, message: "A school with this ID already exists or This school name already exist ");

            }
        }





        public async Task<ServiceResponse<SchoolDto>> UpdateSchoolByIdAsync(Guid id, SchoolDto schoolDto)
        {
            //  throw new NotImplementedException();
            var searchedSchool = await _sr.GetSchoolByIdAsync(id);

            if (searchedSchool == null)
            {
                return new ServiceResponse<SchoolDto>(null!, success: false, message: "School is not included in the list.");

            }


            // Map SchooltDto to School entity
            var school = _mapper.Map<School>(schoolDto);


            await _sr.UpdateSchoolByIdAsync(school);


            // Map the saved entity back to ProductDto
            var updatedSchool = _mapper.Map<SchoolDto>(school);


            return new ServiceResponse<SchoolDto>(updatedSchool, success: true, message: "School was updated successfully!.");
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


