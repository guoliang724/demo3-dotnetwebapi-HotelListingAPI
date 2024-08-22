using HotelListingAPI.Contracts;
using HotelListingAPI.Data;

namespace HotelListingAPI.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRespository
    {
        private readonly HotelListingDbContext _db;
        public HotelRepository(HotelListingDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
