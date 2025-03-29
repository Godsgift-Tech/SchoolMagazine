using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;
//using System.Data.Entity;

namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class AdvertRepository : IAdvertRepository
    {

        private readonly MagazineContext _db;
        public AdvertRepository(MagazineContext db)
        {
            _db = db;
        }


        public async Task AddAdvertAsync(SchoolAdvert advert)
        {
            await _db.Adverts.AddAsync(advert);
            await _db.SaveChangesAsync();
        }

        public async Task<PagedResult<SchoolAdvert>> GetPagedAdvertsAsync(int pageNumber, int pageSize)
        {
            var query = _db.Adverts.Include(a => a.School).AsQueryable();
            return await PagedResult<SchoolAdvert>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<SchoolAdvert?> GetAdvertByIdAsync(Guid advertId)
        {
            return await _db.Adverts
                .Include(a => a.School)
                .FirstOrDefaultAsync(a => a.Id == advertId);
        }

        public async Task<PagedResult<SchoolAdvert>> GetPagedAdvertsBySchoolIdAsync(Guid schoolId, int pageNumber, int pageSize)
        {
            var query = _db.Adverts
                .Where(a => a.SchoolId == schoolId)
                .Include(a => a.School)
                .AsQueryable();

            return await PagedResult<SchoolAdvert>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<PagedResult<SchoolAdvert>> SearchPagedAdvertsAsync(string keyword, int pageNumber, int pageSize)
        {
            var query = _db.Adverts
                .Where(a => a.Title.Contains(keyword) || a.Content.Contains(keyword))
                .Include(a => a.School)
                .AsQueryable();

            return await PagedResult<SchoolAdvert>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<bool> DeleteAdvertAsync(Guid advertId)
        {
            var advert = await _db.Adverts.FindAsync(advertId);
            if (advert == null)
                return false;

            _db.Adverts.Remove(advert);
            await _db.SaveChangesAsync();
            return true;
        }


        public async Task<bool> SchoolExistsAsync(Guid schoolId)
        {
            return await _db.Schools.AnyAsync(s => s.Id == schoolId);
        }


    }
}