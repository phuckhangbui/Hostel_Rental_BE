using BusinessObject.Models;
using DTOs.Room;

namespace Repository.Interface
{
	public interface IRoomRepository
	{
		Task<int> CreateRoom(CreateRoomRequestDto createRoomRequestDto);
		Task<Room> GetRoomById(int roomId);
		Task UpdateRoom(int roomId, UpdateRoomRequestDto updateRoomRequestDto);
		Task<IEnumerable<RoomListResponseDto>> GetListRoomsByHostelId(int hostelId);
		Task<RoomDetailResponseDto> GetRoomDetailById(int roomId);
		Task UpdateRoomStatus(int roomId, int status);
		Task UploadRoomImage(int roomId, List<string> imageUrls);
		Task<IEnumerable<RoomOfHostelAdminView>> GetHostelDetailWithRoomAdminView(int hostelId);
    }
}
