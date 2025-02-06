using SchoolMagazine.Domain.Entities;

namespace SchoolMagazine.Domain.Interface
{
    public interface IAdvertRepository
    {
        Task<IEnumerable<SchoolAdvert>> GetAllAdvertsAsync();
        Task<School> GetAdvertBySchool( Guid id, SchoolAdvert schooladvert);
        Task AddSchoolAdvertAsync(SchoolAdvert schooladvert);
        Task UpdateSchoolAdvertAsync(Guid id, SchoolAdvert schooladvert);
        Task DeleteSchoolAdvertAsync(Guid id, SchoolAdvert schooladvert);


    }
}
