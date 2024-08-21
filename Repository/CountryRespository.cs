using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Repository
{
    public class CountryRespository : GenericRepository<Country>, ICountriesRespository
    {
        private readonly HotelListingDbContext _db;
        public CountryRespository(HotelListingDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _db.countries.Include(q=>q.Hotels).FirstOrDefaultAsync(q=>q.Id == id);  
        }
    }
}
