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
	}
}
