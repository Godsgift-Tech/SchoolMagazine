﻿using AutoMapper;
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

            var school = _mapper.Map<School>(schoolDto);


            await _sr.AddSchoolAsync(school);


            // Map the saved entity back to ProductDto
            var createdSchool = _mapper.Map<SchoolDto>(school);

            return new ServiceResponse<SchoolDto>(createdSchool, success: true, message: "School was created successfully!.");
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
        public async Task<ServiceResponse<bool>> DeleteSchoolByIdAsync(Guid id, SchoolDto schoolDto)
        {
            var searchedSchool = await _sr.GetSchoolByIdAsync(id);

            if (searchedSchool == null)
            {
                return new ServiceResponse<bool>(false, success: false, message: "School was not found.");

            }

            await _sr.DeleteSchoolByIdAsync(searchedSchool);


            return new ServiceResponse<bool>(true, success: true, message: "School was removed successfully.");
        }





    }

}
