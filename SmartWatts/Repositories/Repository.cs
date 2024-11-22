using Microsoft.EntityFrameworkCore;
using SmartWatts.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWatts.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext DbContext;

        public Repository(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await DbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                DbContext.Set<T>().Remove(entity);
                await DbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }
        }
    }
}
