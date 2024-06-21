using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class ComplainDao : BaseDAO<Complain>
    {
        private static ComplainDao instance = null;
        private static readonly object instacelock = new object();

        public ComplainDao()
        {
        }

        public static ComplainDao Instance
        {
            get
            {
                lock (instacelock)
                {
                    if (instance == null)
                    {
                        instance = new ComplainDao();
                    }
                    return instance;
                }

            }
        }

        public async Task<Complain> GetComplainById(int id)
        {
            Complain complain = null;
            using (var context = new DataContext())
            {
                complain = await context.Complain.Include(c => c.Room).Include(c => c.ComplainAccount).FirstOrDefaultAsync(x => x.ComplainID == id);
            }
            return complain;
        }

        public async Task<IEnumerable<Complain>> GetComplainWithOnwerId()
        {
            var complains = new List<Complain>();
            using (var context = new DataContext())
            {
                complains = await context.Complain.Include(c => c.ComplainAccount).Include(c => c.Room).ThenInclude(r => r.Hostel).ThenInclude(h => h.OwnerAccount)
                    .ToListAsync();
            }
            return complains;
        }
    }
}
