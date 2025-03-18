using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly MagazineContext _db;
        public AdvertRepository(MagazineContext db)
        {
            _db = db;
        }

       
        public async Task<PagedResult<SchoolAdvert>> GetAllAdvertsAsync(int pageNumber, int pageSize)
        {
            var query = _db.Adverts.Include(a => a.School).AsQueryable();

            int totalCount = await query.CountAsync();

            var adverts = await query
                .OrderByDescending(a => a.StartDate)  // Sort by latest adverts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<SchoolAdvert>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = adverts
            };
        }


        public async Task<IEnumerable<SchoolAdvert>> GetAdvertBySchoolAsync(string schoolName)
        {
            //  return await _db.Adverts.Where(a => a.SchoolName == schoolName).ToListAsync();


            return await _db.Adverts
               .Include(e => e.School)  // Include School entity
               .Where(e => e.School.SchoolName == schoolName) // Filter by SchoolName
               .ToListAsync();
        }
        public async Task<IEnumerable<SchoolAdvert>> GetAllPaidAdvertsAsync()
        {
            return await _db.Adverts
                .Where(ad => ad.IsPaid) // Filter only paid adverts
                .ToListAsync();
        }

        public async Task<AdvertServiceResponse<SchoolAdvert>> AddSchoolAdvertAsync(SchoolAdvert advertDetails)
        {
            _db.Adverts.Add(advertDetails);
            await _db.SaveChangesAsync();
            return new AdvertServiceResponse<SchoolAdvert>
            {
                Data = advertDetails,
                Message = "Advert added successfully",
                Success = true
            };
        }

        public async Task<bool> SchoolExistsAsync(Guid schoolId)
        {
            return await _db.Schools.AnyAsync(s => s.Id == schoolId);
        }

        public async Task<SchoolAdvert?> GetAdvertByIdAsync(Guid id)
        {
            return await _db.Adverts.FindAsync(id);
        }

        public async Task<SchoolAdvert?> GetAdvertByTitleAsync(string title, Guid schoolId)
        {
            return await _db.Adverts
                .FirstOrDefaultAsync(e => e.Title == title && e.SchoolId == schoolId);
        }

        public async Task<IEnumerable<SchoolAdvert>> GetAdvertBySchoolId(Guid schoolId)
        {
            return await _db.Adverts
               .Include(e => e.School) // ✅ Load related school data
                .Where(e => e.SchoolId == schoolId) // 🔍 Filter events by school ID
                .ToListAsync();
        }
        public async Task UpdateSchoolAdvertAsync(SchoolAdvert advertDetails)
        {
            _db.Adverts.Update(advertDetails);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteSchoolAdvertAsync(Guid advertId)
        {
            var advert = await _db.Adverts.FindAsync(advertId);
            if (advert != null)
            {
                _db.Adverts.Remove(advert);
                await _db.SaveChangesAsync();
            }
        }

    }

}