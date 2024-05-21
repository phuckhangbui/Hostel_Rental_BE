using AutoMapper;
using BusinessObject.Enum;
using BusinessObject.Models;
using DTOs;
using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
	public class RoomService : IRoomService
	{
		private readonly IRoomRepository _roomRepository;
		private readonly ICloudinaryService _cloudinaryService;
		private readonly IMapper _mapper;

		public RoomService(IRoomRepository roomRepository, ICloudinaryService cloudinaryService, IMapper mapper)
		{
			_roomRepository = roomRepository;
			_mapper = mapper;
			_cloudinaryService = cloudinaryService;
		}

		public async Task CreateRoom(CreateRoomRequestDto createRoomRequestDto)
		{
			Room room = new Room
			{
				RoomName = createRoomRequestDto.RoomName,
				Capacity = createRoomRequestDto.Capacity,
				Lenght = createRoomRequestDto.Lenght,
				Width = createRoomRequestDto.Width,
				Description = createRoomRequestDto.Description,
				RoomFee = createRoomRequestDto.RoomFee,
				HostelID = createRoomRequestDto.HostelID,
				Status = (int)RoomEnum.Available,
				RoomImages = new List<RoomImage>(),
		};

			await _roomRepository.CreateRoom(room);
		}

		public async Task UploadRoomImage(IFormFileCollection files, int roomId)
		{
			Room room = await _roomRepository.GetRoomById(roomId);
			if (room != null)
			{
				try
				{
					if (room.RoomImages == null)
					{
						room.RoomImages = new List<RoomImage>();
					}

					foreach (var file in files)
					{
						var result = await _cloudinaryService.AddPhotoAsync(file);
						if (result.Error != null)
						{
							throw new ServiceException("Error uploading image to Cloudinary: " + result.Error.Message);
						}

						string imageUrl = result.SecureUrl.AbsoluteUri;
						var roomImage = new RoomImage
						{
							RoomUrl = imageUrl,
							RoomID = room.RoomID 
						};

						room.RoomImages.Add(roomImage);
					}

					await _roomRepository.UpdateRoom(room);
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
	}
}
