using SchoolMagazine.Domain.Entities;

namespace SchoolMagazine.Domain.RepositoryInterface
{
    public interface ISchoolRatingRepository
    {
        /// <summary>
        /// Adds a new rating to the database.
        /// </summary>
        Task AddRatingAsync(SchoolRating rating);

        /// <summary>
        /// Retrieves all ratings from the database.
        /// </summary>
        Task<List<SchoolRating>> GetAllRatingsAsync();

        /// <summary>
        /// Gets all ratings for a specific school by SchoolId.
        /// </summary>
        Task<List<SchoolRating>> GetRatingsForSchoolAsync(Guid schoolId);

        /// <summary>
        /// Gets all schools that have ratings and includes their rating navigation.
        /// </summary>
        Task<List<School>> GetSchoolsWithRatingsAsync();

        /// <summary>
        /// Gets a school entity by its name, including ratings.
        /// </summary>
        /// <param name="schoolName">The name of the school (case-insensitive).</param>
        Task<School?> GetSchoolByNameAsync(string schoolName);
        //


    }
}
