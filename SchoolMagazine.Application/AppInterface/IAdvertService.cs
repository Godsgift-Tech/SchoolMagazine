using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IAdvertService
    {
        Task<IEnumerable<SchoolAdvertDto>> GetAllAdvertsAsync();
        Task<SchoolDto> GetAdvertBySchool(Guid id, SchoolAdvertDto schooladvertDto);
        Task AddSchoolAdvertAsync(SchoolAdvertDto schooladvertDto);
        Task UpdateSchoolAdvertAsync(Guid id, SchoolAdvertDto schooladvertDto);
        Task DeleteSchoolAdvertAsync(Guid id, SchoolAdvertDto schooladvertDto);


    }



}
