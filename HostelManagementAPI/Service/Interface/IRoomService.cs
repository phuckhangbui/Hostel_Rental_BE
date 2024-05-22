using DTOs.Room;
using Microsoft.AspNetCore.Http;

namespace Service.Interface
{
    public interface IRoomService
	{
		Task CreateRoom(CreateRoomRequestDto createRoomRequestDto);
		Task UploadRoomImage(IFormFileCollection files, int roomId);
		Task <IEnumerable<RoomListResponseDto>> GetListRoomsByHostelId(int hostelId);
		Task <RoomDetailResponseDto> GetRoomDetailByRoomId(int roomId);
		Task ChangeRoomStatus(int roomId, int status);
	}
}
