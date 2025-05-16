using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.RepositoryInterface;
using SchoolMagazine.Domain.UserRoleInfo;

namespace SchoolMagazine.Infrastructure.Data.RepositoryImplementation
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly MagazineContext _db;

        public AppUserRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _db.Users.FindAsync(userId);
        }

        public async Task<List<User>> GetUsersByIdsAsync(IEnumerable<Guid> userIds)
        {
            return await _db.Users
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();
        }
    }
}
