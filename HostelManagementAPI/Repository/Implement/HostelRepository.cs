using BusinessObject.Models;
using DAO;
using Repository.Interface;

namespace Repository.Implement
{
    public class HostelRepository : IHostelRepository
	{
		public async Task<bool> CreateHostel(Hostel hostel)
		{
			return await HostelDao.Instance.CreateAsync(hostel);
		}

		public async Task<IEnumerable<Hostel>> GetAllHostels()
		{
			return await HostelDao.Instance.GetAllHostelsAsync();
		}

		public async Task<Hostel> GetHostelById(int id)
		{
			return await HostelDao.Instance.GetHostelById(id);
		}

		public async Task UpdateHostel(Hostel hostel)
		{
			await HostelDao.Instance.UpdateAsync(hostel);
		}
	}
}
