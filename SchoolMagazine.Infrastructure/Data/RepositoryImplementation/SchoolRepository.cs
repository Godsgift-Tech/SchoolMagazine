using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;
//using System.Data.Entity;
//using System.Data.Entity;

//using System.Data.Entity;  =>   This using gives me Server error 500

namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly MagazineContext _db;
        public SchoolRepository(MagazineContext db)
        {
            _db = db;
        }


        public async Task<ServiceResponse<School>> AddSchoolAsync(School school)
        {
            var existingSchoolById = await _db.Schools.FindAsync(school.Id);
            if (existingSchoolById != null)
                //  return new ServiceResponse<School> { Success = false, Message = "School ID already exists." };
                return new ServiceResponse<School>(null!, success: false, message: "School ID already exists.");

            var existingSchoolByName = await _db.Schools.FirstOrDefaultAsync(s => s.SchoolName == school.SchoolName);
            if (existingSchoolByName != null)
                //   return new ServiceResponse<School> { Success = false, Message = "School name already exists." };
                return new ServiceResponse<School>(null!, success: false, message: "School name already exists.");


            await _db.Schools.AddAsync(school);
            await _db.SaveChangesAsync();
            return new ServiceResponse<School>(null!, success: true, message: "School was registered successfully!!");

        }
       

        public async Task<string> UpdateSchoolAsync(School school)
        {
            _db.Schools.Update(school);
            var result = await _db.SaveChangesAsync();

            return result > 0 ? "School updated successfully." : "Failed to update school.";
        }


        public async Task DeleteSchoolByIdAsync(School searchedSchool)
        {
            var schoolId = await _db.Schools.FindAsync(searchedSchool.Id);
            if (schoolId == null) throw new Exception("School was not found");

            _db.Schools.Remove(searchedSchool);
            await _db.SaveChangesAsync();

        }


        public async Task<School?> GetSchoolByNameAsync(string schoolName)
        {
            return await _db.Schools
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SchoolName.ToLower() == schoolName.ToLower());
        }

        public async Task<List<School>> GetSchoolsByLocationAsync(string location)
        {
            return await _db.Schools
                .AsNoTracking()
                .Where(s => s.Location.ToLower() == location.ToLower())
                .ToListAsync();
        }

       

        public async Task<School> GetSchoolByIdAsync(Guid id)
        {
            return await _db.Schools.FindAsync(id);
        }



        public async Task<PagedResult<School>> GetPagedResultAsync(int pageNumber, int pageSize)
        {
            var query = _db.Schools.AsQueryable();

            int totalCount = await query.CountAsync();

            var schools = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Explicitly load adverts for each school
            foreach (var school in schools)
            {
                await _db.Entry(school).Collection(s => s.Adverts).LoadAsync();
            }

            return new PagedResult<School>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = schools
            };
        }

        public async Task<List<School>> GetSchoolsByFeesRangeAsync(decimal feesRange)
        {
            return await _db.Schools
                .AsNoTracking()
                .Where(s => s.FeesRange >= (feesRange - 500) && s.FeesRange <= (feesRange + 500))
                .ToListAsync();
        }

        public async Task<List<School>> GetSchoolsByRatingAsync(double rating)
        {
            return await _db.Schools
                .AsNoTracking()
                .Where(s => s.Rating >= (rating - 0.5) && s.Rating <= (rating + 0.5))
                .ToListAsync();
        }


        public async Task<PagedResult<School>> GetSchoolsAsync(string? schoolName, string? location, decimal? feesRange, double? rating, int pageNumber, int pageSize)
        {
            var query = _db.Schools.Include(s => s.Adverts).AsQueryable();

            if (!string.IsNullOrWhiteSpace(schoolName))
                query = query.Where(s => s.SchoolName.Contains(schoolName));

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(s => s.Location.Contains(location));

            if (feesRange.HasValue)
                query = query.Where(s => s.FeesRange == feesRange.Value);

            if (rating.HasValue)
                query = query.Where(s => s.Rating >= rating.Value);

            int totalCount = await query.CountAsync();

            var schools = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            

                return new PagedResult<School>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = schools
            };
        }


    }
}
