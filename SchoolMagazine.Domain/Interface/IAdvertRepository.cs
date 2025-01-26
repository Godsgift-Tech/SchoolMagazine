using SchoolMagazine.Domain.Entities;

namespace SchoolMagazine.Domain.Interface
{
    public interface IAdvertRepository
    {
        Task<IEnumerable<SchoolAdvert>> GetAllAdvertsAsync();
        Task<SchoolAdvert> GetAdvertBySchool(School school);
        Task AddSchoolAdvertAsync(SchoolAdvert schooladvert);


    }
}
