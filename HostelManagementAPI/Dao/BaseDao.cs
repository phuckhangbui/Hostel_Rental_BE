using Microsoft.EntityFrameworkCore;

namespace DAO
{

    public class BaseDAO<T> where T : class
    {
        public async Task<IList<T>> GetAllAsync()
        {
            List<T> list;
            try
            {
                var _context = new DataContext();
                DbSet<T> _dbSet = _context.Set<T>();
                list = await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }

        public async Task CreateAsync(T entity)
        {
            try
            {
                var _context = new DataContext();
                DbSet<T> _dbSet = _context.Set<T>();
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                var _context = new DataContext();
                DbSet<T> _dbSet = _context.Set<T>();
                var tracker = _context.Attach(entity);
                tracker.State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveAsync(T entity)
        {
            try
            {
                var _context = new DataContext();
                DbSet<T> _dbSet = _context.Set<T>();
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
