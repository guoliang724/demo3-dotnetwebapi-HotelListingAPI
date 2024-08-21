using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListingAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly HotelListingDbContext _db;
        public GenericRepository(HotelListingDbContext db) {
            _db = db;
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T item)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync(T item)
        {
            _db.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
              var entity = await GetAsync(id);
            if (entity != null) {
                _db.Set<T>().Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            var entiry = await GetAsync(id);
            return entiry != null;
        }
    }
}
