using BusinessObject.Models;
using DAO;
using Repository.Interface;

namespace Repository.Implement
{
	public class RoomRepository : IRoomRepository
	{
		public async Task<bool> CreateRoom(Room room)
		{
			return await RoomDao.Instance.CreateAsync(room);
		}

		public async Task<Room> GetRoomById(int roomId)
		{
			return await RoomDao.Instance.GetRoomById(roomId);
		}

		public async Task UpdateRoom(Room room)
		{
			await RoomDao.Instance.UpdateAsync(room);
		}
	}
}
