using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;

namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly MagazineContext _db;
        public AdvertRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task AddSchoolAdvertAsync(SchoolAdvert schooladvert)
        {
            _db.Adverts.Add(schooladvert);
            await _db.SaveChangesAsync();  //saves advert to database
        }

        public async Task<SchoolAdvert> GetAdvertBySchool(School school)
        {
            return await _db.Adverts.FindAsync(school);

        }

        public async Task<IEnumerable<SchoolAdvert>> GetAllAdvertsAsync()
        {
          return await (_db.Adverts.ToListAsync());
        }
    }


}
