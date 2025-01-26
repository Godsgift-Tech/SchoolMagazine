using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;

namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly MagazineContext _db;
        public SchoolRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task AddSchoolAsync(School school)
        {
            _db.Schools.Add(school);
            await _db.SaveChangesAsync();

        }

        public async Task<IEnumerable<School>> GetAllSchoolAsync()
        {
            return await _db.Schools.ToListAsync();
        }

        public async Task<School> GetSchoolByIdAsync(Guid id)
        {
            return await _db.Schools.FindAsync(id);
        }
    }
}
