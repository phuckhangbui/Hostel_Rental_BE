using DTOs;
using Microsoft.AspNetCore.Http;

namespace Service.Interface
{
	public interface IRoomService
	{
		Task CreateRoom(CreateRoomRequestDto createRoomRequestDto);
		Task UploadRoomImage(IFormFileCollection files, int roomId);
	}
}
