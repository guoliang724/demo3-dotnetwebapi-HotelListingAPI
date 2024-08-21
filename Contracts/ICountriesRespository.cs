using HotelListingAPI.Data;

namespace HotelListingAPI.Contracts
{
    public interface ICountriesRespository : IGenericRepository<Country> {
        Task<Country> GetDetails(int id);
    }
}
