using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.RepositoryInterface;

namespace SchoolMagazine.Infrastructure.Data.RepositoryImplementation
{
    public class SchoolRatingRepository : ISchoolRatingRepository
    {
        private readonly MagazineContext _context;

        public SchoolRatingRepository(MagazineContext context)
        {
            _context = context;
        }

        public async Task AddRatingAsync(SchoolRating rating)
        {
            await _context.SchoolRatings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SchoolRating>> GetAllRatingsAsync()
        {
            return await _context.SchoolRatings
                .Include(r => r.School)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<List<SchoolRating>> GetRatingsForSchoolAsync(Guid schoolId)
        {
            return await _context.SchoolRatings
                .Where(r => r.SchoolId == schoolId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<List<School>> GetSchoolsWithRatingsAsync()
        {
            return await _context.Schools
                .Include(s => s.Ratings)
                .ToListAsync();
        }

        public async Task<School?> GetSchoolByNameAsync(string schoolName)
        {
            return await _context.Schools
                .Include(s => s.Ratings)
                .FirstOrDefaultAsync(s => s.SchoolName.ToLower() == schoolName.ToLower());
        }
    }
}
