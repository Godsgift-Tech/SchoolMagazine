using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.UserRoleInfo;

namespace SchoolMagazine.Domain.RepositoryInterface
{
    public interface IAppUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId);
        Task<List<User>> GetUsersByIdsAsync(IEnumerable<Guid> userIds);
    }
}
