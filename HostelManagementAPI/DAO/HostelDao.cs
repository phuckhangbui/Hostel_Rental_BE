using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
	public class HostelDao : BaseDAO<Hostel>
	{
		private static HostelDao instance = null;
		private readonly DataContext dataContext;

		private HostelDao()
		{
			dataContext = new DataContext();
		}

		public static HostelDao Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new HostelDao();
				}
				return instance;
			}
		}

		public async Task<Hostel> GetHostelById(int id)
		{
			return await dataContext.Hostel
				.Include(x => x.OwnerAccount)
                .Include(h => h.Rooms)
				.FirstOrDefaultAsync(h => h.HostelID == id);
		}

		public async Task<IEnumerable<Hostel>> GetAllHostelsAsync()
		{
			return await dataContext.Hostel
				.Include(h => h.OwnerAccount)
				.Include(h => h.Rooms)
				.ToListAsync();
		}

        public async Task<IEnumerable<Hostel>> GetAllHostelsTotalActiveAsync()
        {
            return await dataContext.Hostel.Where(x => x.Status == 0)
                .ToListAsync();
        }

    }
}
