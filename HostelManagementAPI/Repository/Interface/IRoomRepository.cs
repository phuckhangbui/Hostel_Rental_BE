﻿using BusinessObject.Models;

namespace Repository.Interface
{
	public interface IRoomRepository
	{
		Task<bool> CreateRoom(Room room);
		Task<Room> GetRoomById(int roomId);
		Task UpdateRoom(Room room);
		Task<IEnumerable<Room>> GetListRoomsByHostelId(int hostelId);
		Task<Room> GetRoomDetailById(int roomId);
	}
}
