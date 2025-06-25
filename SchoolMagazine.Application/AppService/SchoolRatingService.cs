using AutoMapper;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService
{
    public class SchoolRatingService : ISchoolRatingService
    {
        private readonly ISchoolRatingRepository _ratingRepo;
        private readonly IMapper _mapper;

        public SchoolRatingService(ISchoolRatingRepository ratingRepo, IMapper mapper)
        {
            _ratingRepo = ratingRepo;
            _mapper = mapper;
        }

        public async Task AddRatingAsync(RateSchoolDto dto)
        {
            var school = await _ratingRepo.GetSchoolByNameAsync(dto.SchoolName);
            if (school == null)
                throw new Exception("School not found.");

            var rating = new SchoolRating
            {
                SchoolId = school.Id,
                UserId = dto.UserId,
                SchoolAdminId = dto.SchoolAdminId,
                RatingValue = dto.RatingValue,
                Comment = dto.Comment,
                RatedAt = DateTime.UtcNow
            };

            await _ratingRepo.AddRatingAsync(rating);
        }

        public async Task<List<SchoolRatingDto>> GetAllRatingsAsync()
        {
            var ratings = await _ratingRepo.GetAllRatingsAsync();
            return _mapper.Map<List<SchoolRatingDto>>(ratings);
        }

        public async Task<List<SchoolRatingDto>> GetRatingsForSchoolAsync(string schoolName)
        {
            var school = await _ratingRepo.GetSchoolByNameAsync(schoolName);
            if (school == null)
                throw new Exception("School not found.");

            var ratings = await _ratingRepo.GetRatingsForSchoolAsync(school.Id);
            return _mapper.Map<List<SchoolRatingDto>>(ratings);
        }

        public async Task<List<SchoolWithRatingDto>> GetSchoolsByAverageRatingAsync(double minRating)
        {
            var schools = await _ratingRepo.GetSchoolsWithRatingsAsync();

            var result = schools
                .Select(s => new SchoolWithRatingDto
                {
                    SchoolId = s.Id,
                    SchoolName = s.SchoolName,
                    AverageRating = s.Ratings.Any() ? s.Ratings.Average(r => r.RatingValue) : 0
                })
                .Where(s => s.AverageRating >= minRating)
                .ToList();

            return result;
        }
    }
}
