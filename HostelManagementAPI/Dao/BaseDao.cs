using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Dao
{
    public class BaseDao<T> where T : class
    {

        private static HostelManagementDBContext _context = null;
        private static BaseDao<T> instance = null;

        public BaseDao()
        {
            _context = new HostelManagementDBContext();
        }

        public static BaseDao<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    return instance = new BaseDao<T>();
                }
                else { return instance; }
            }
        }

        public virtual IEnumerable<T> getListObject()
        {
            return _context.Set<T>().ToList();
        }

        public bool createObject(T entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updateObject(T entity)
        {
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
