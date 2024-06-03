using BusinessObject.Models;
using DTOs.Enum;
using DTOs.Room;
using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class RoomService : IRoomService
    {
		private readonly IRoomRepository _roomRepository;
		private readonly IHostelRepository _hostelRepository;
		private readonly ICloudinaryService _cloudinaryService;

		public async Task<IEnumerable<RoomListResponseDto>> GetListRoomsByHostelId(int hostelId)
		{
			return await _roomRepository.GetListRoomsByHostelId(hostelId);
		}

		public RoomService(
			IRoomRepository roomRepository, 
			IHostelRepository hostelRepository,
			ICloudinaryService cloudinaryService)
		{
			_roomRepository = roomRepository;
			_hostelRepository = hostelRepository;
			_cloudinaryService = cloudinaryService;
		}

		public async Task ChangeRoomStatus(int roomId, int status)
		{
			var room = await _roomRepository.GetRoomDetailById(roomId);
			if (room == null)
			{
				throw new ServiceException("Room not found with this ID");
			}

			if (!Enum.IsDefined(typeof(RoomEnum), status))
			{
				throw new ServiceException("Invalid status value");
			}

			await _roomRepository.UpdateRoomStatus(roomId, status);
		}

		public async Task<RoomDetailResponseDto> GetRoomDetailByRoomId(int roomId)
		{
			var room = await _roomRepository.GetRoomDetailById(roomId);
			if (room == null)
			{
				throw new ServiceException("Room not found with this ID");
			}

			return room;
		}

		public async Task<CreateRoomResponseDto> CreateRoom(CreateRoomRequestDto createRoomRequestDto)
		{
			var hostel = await _hostelRepository.GetHostelById(createRoomRequestDto.HostelID);
			if (hostel == null)
			{
				throw new ServiceException("Hostel not found with this ID");
			}

			var roomId = await _roomRepository.CreateRoom(createRoomRequestDto);

			return new CreateRoomResponseDto { RoomID = roomId };
		}

		public async Task UploadRoomImage(IFormFileCollection files, int roomId)
		{
			var room = await _roomRepository.GetRoomDetailById(roomId);
			if (room != null)
			{
				try
				{
					var images = new List<string>();
					foreach (var file in files)
					{
						var result = await _cloudinaryService.AddPhotoAsync(file);
						if (result.Error != null)
						{
							throw new ServiceException("Error uploading image to Cloudinary: " + result.Error.Message);
						}

						string imageUrl = result.SecureUrl.AbsoluteUri;
						images.Add(imageUrl);
					}

					await _roomRepository.UploadRoomImage(roomId, images);
				}
				catch (Exception ex)
				{
					throw new ServiceException("Upload room image fail with error", ex);
				}
			}
			else
			{
				throw new ServiceException("Room not found with this ID");
			}
		}

		public async Task UpdateRoom(int roomId, UpdateRoomRequestDto updateRoomRequestDto)
		{
			var room = await _roomRepository.GetRoomDetailById(roomId);
			if (room == null)
			{
				throw new ServiceException("Room not found with this ID");
			}

			await _roomRepository.UpdateRoom(roomId, updateRoomRequestDto);
		}

        public async Task<IEnumerable<RoomOfHostelAdminView>> GetHostelDetailWithRoomAdminView(int hostelId)
        {
            return await _roomRepository.GetHostelDetailWithRoomAdminView(hostelId); ;
        }

        public async Task<List<string>> GetRoomImagesByHostelId(int hostelId)
        {
			return await _roomRepository.GetRoomImagesByHostelId(hostelId);
        }

        public Task AddRoomService(AddRoomServicesDto addRoomServicesDto)
        {
			return _roomRepository.AddRoomServicesAsync(addRoomServicesDto); ;
        }

        public Task RemoveRoomServiceAsync(int roomId, int serviceId)
        {
            return _roomRepository.RemoveRoomServiceAsync(roomId, serviceId);
        }
    }
}
