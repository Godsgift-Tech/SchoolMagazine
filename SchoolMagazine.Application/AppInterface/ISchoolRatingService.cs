using SchoolMagazine.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppInterface
{
    public interface ISchoolRatingService
    {
        //Task AddRatingAsync(Guid userId, string schoolName, int ratingValue, string? comment);
        //Task<List<SchoolWithRatingDto>> GetSchoolsByAverageRatingAsync(double minRating);
        //Task AddRatingAsync(RateSchoolDto dto);

        /// <summary>
        /// Allows a school admin or registered user to rate a school using a school name.
        /// </summary>
        /// <param name="dto">Rating data including user/admin ID, school name, rating value, and comment.</param>
        Task AddRatingAsync(RateSchoolDto dto);

        /// <summary>
        /// Returns all ratings made by users and school admins.
        /// </summary>
        /// <returns>List of all school ratings.</returns>
        Task<List<SchoolRatingDto>> GetAllRatingsAsync();

        /// <summary>
        /// Returns all schools with their average ratings above a minimum threshold.
        /// </summary>
        /// <param name="minRating">Minimum average rating to filter schools.</param>
        /// <returns>List of schools with average rating >= minRating.</returns>
        Task<List<SchoolWithRatingDto>> GetSchoolsByAverageRatingAsync(double minRating);

        /// <summary>
        /// Returns all individual ratings for a specific school by name.
        /// </summary>
        /// <param name="schoolName">The name of the school.</param>
        /// <returns>List of individual ratings for the school.</returns>
        Task<List<SchoolRatingDto>> GetRatingsForSchoolAsync(string schoolName);
    }
}
