using DTOs.Room;
using Microsoft.AspNetCore.Http;

namespace Service.Interface
{
    public interface IRoomService
	{
		Task<CreateRoomResponseDto> CreateRoom(CreateRoomRequestDto createRoomRequestDto);
		Task UploadRoomImage(IFormFileCollection files, int roomId);
		Task <IEnumerable<RoomListResponseDto>> GetListRoomsByHostelId(int hostelId);
        Task<IEnumerable<RoomOfHostelAdminView>> GetHostelDetailWithRoomAdminView(int hostelId);
        Task <RoomDetailResponseDto> GetRoomDetailByRoomId(int roomId);
		Task ChangeRoomStatus(int roomId, int status);
		Task UpdateRoom(int roomId, UpdateRoomRequestDto updateRoomRequestDto);
		Task<List<string>> GetRoomImagesByHostelId(int hostelId);
		//Task AddRoomService(AddRoomServicesDto addRoomServicesDto);
		//Task RemoveRoomServiceAsync(int roomId, int serviceId);
  //      Task<IEnumerable<RoomServiceResponseDto>> GetRoomServicesByRoomIdAsync(int roomId);
    }
}
