using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
	public class RoomDao : BaseDAO<Room>
	{
		private static RoomDao instance = null;
		private readonly DataContext dataContext;

		private RoomDao()
		{
			dataContext = new DataContext();
		}

		public static RoomDao Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new RoomDao();
				}
				return instance;
			}
		}

		public async Task<Room> GetRoomById(int roomId)
		{
			return await dataContext.Room.FirstOrDefaultAsync(r => r.RoomID == roomId);
		}

		public async Task<IEnumerable<Room>> GetRoomListByHostelId(int hostelId)
		{
            using (var context = new DataContext())
			{
                return await context.Room
                .Where(r => r.HostelID == hostelId)
                .Include(r => r.RoomImages)
                .ToListAsync();
            }
		}

		public async Task<Room> GetRoomDetailById(int roomId)
		{
            using (var context = new DataContext())
			{
                return await context.Room
					.Include(r => r.RoomImages)
					.Include(r => r.RoomServices)
						.ThenInclude(rs => rs.Service)
					.FirstOrDefaultAsync(r => r.RoomID == roomId);
            }
		}

        public async Task<List<string>> GetRoomImagesByHostelId(int hostelId)
        {
            return await dataContext.Room
                                    .Where(r => r.HostelID == hostelId)
                                    .Join(dataContext.RoomsImage,
                                          r => r.RoomID,
                                          ri => ri.RoomID,
                                          (r, ri) => ri.RoomUrl)
                                    .ToListAsync();
        }
    }
}
