using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class AdvertRepository : IAdvertRepository
    {
        private readonly MagazineContext _db;
        public AdvertRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task AddSchoolAdvertAsync(SchoolAdvert schooladvert)
        {
            _db.Adverts.Add(schooladvert);
            await _db.SaveChangesAsync();  //saves advert to database
        }

        public async Task<School> GetAdvertBySchool(Guid id, SchoolAdvert schooladvert)
        {
            //if(schooladvert==null) throw new Exception("No Advert was found for:" + (nameof(schooladvert)));
           // return await _db.Adverts.FindAsync(id);
            //
            if (schooladvert.SchoolName == null) throw new Exception("There was no advert posted for this school");

          

            return await _db.Schools.FindAsync(schooladvert);

        }

        public async Task UpdateSchoolAdvertAsync(Guid id, SchoolAdvert schooladvert)
        {
          
            
            var advert = await _db.Adverts.FindAsync(id);

            if (advert == null) throw new Exception("Advert not found");

        

            //advert.EndDate = schooladvert.EndDate;
            //advert.StartDate = schooladvert.StartDate;
            //advert.SchoolId = schooladvert.SchoolId;
            //advert.SchoolName = schooladvert.SchoolName;
            //advert.Content = schooladvert.Content;  
            //advert.Title = schooladvert.Title;
             if (schooladvert == null)
            {
                throw new Exception("Advert not found");
            }


            _db.Adverts.Update(advert);
            await _db.SaveChangesAsync();



        }
        public async Task DeleteSchoolAdvertAsync(Guid id, SchoolAdvert schooladvert)
        {


            var advert = await _db.Adverts.FindAsync(id);

            if (advert == null) throw new Exception("Advert not found");


            if (schooladvert == null)
            {
                throw new Exception("Advert not found");
            }


            _db.Adverts.Remove(advert);
            await _db.SaveChangesAsync();



        }

        public async Task<IEnumerable<SchoolAdvert>> GetAllAdvertsAsync()
        {
          return await (_db.Adverts.ToListAsync());
        }
    }


}
