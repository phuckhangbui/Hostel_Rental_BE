﻿using DTOs.Enum;
using DTOs.Room;
using DTOs.RoomAppointment;
using DTOs.RoomService;
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
		//private readonly IAccountRepository _accountRepository;
		//private readonly IContractRepository _contractRepository;
		private readonly ICloudinaryService _cloudinaryService;

		public RoomService(
			IRoomRepository roomRepository, 
			IHostelRepository hostelRepository,
			//IAccountRepository accountRepository,
			//IContractRepository contractRepository,
			ICloudinaryService cloudinaryService)
		{
			_roomRepository = roomRepository;
			_hostelRepository = hostelRepository;
			//_accountRepository = accountRepository;
			//_contractRepository = contractRepository;
			_cloudinaryService = cloudinaryService;
		}

        public async Task<IEnumerable<RoomListResponseDto>> GetListRoomsByHostelId(int hostelId)
        {
            return await _roomRepository.GetListRoomsByHostelId(hostelId);
        }

        public async Task ChangeRoomStatus(int roomId, int status)
		{
			var room = await _roomRepository.GetRoomById(roomId);
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
			var room = await _roomRepository.GetRoomById(roomId);
			if (room == null)
			{
				throw new ServiceException("Room not found with this ID");
			}

			var roomDetail =  await _roomRepository.GetRoomDetailById(roomId);
			if (roomDetail.Status == (int)RoomEnum.Hiring)
			{
				//var contracts = await _contractRepository.GetContractsAsync();
				//var currentContract = contracts.FirstOrDefault(c => c.RoomID == roomId && c.Status == 1);
				//if (currentContract != null)
				//{
				//	var renterAcocunt = await _accountRepository.GetAccountById((int)currentContract.StudentAccountID);
				//	roomDetail.RenterName = renterAcocunt.Name;
				//}
			}

			return roomDetail;
		}

		public async Task<CreateRoomResponseDto> CreateRoom(CreateRoomRequestDto createRoomRequestDto)
		{
			var hostel = await _hostelRepository.GetHostelDetailById(createRoomRequestDto.HostelID);
			if (hostel == null)
			{
				throw new ServiceException("Hostel not found with this ID");
			}

			var roomId = await _roomRepository.CreateRoom(createRoomRequestDto);

			return new CreateRoomResponseDto { RoomID = roomId };
		}

		public async Task UploadRoomImage(IFormFileCollection files, int roomId)
		{
			var room = await _roomRepository.GetRoomById(roomId);
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

		public async Task UpdateRoom(int roomId, RoomRequestDto updateRoomRequestDto)
		{
			var room = await _roomRepository.GetRoomById(roomId);
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

        public async Task<IEnumerable<GetAppointmentDto>> GetRoomAppointmentsAsync()
        {
            return await _roomRepository.GetRoomAppointmentsAsync();
        }

        public async Task<GetAppointmentDto> GetAppointmentById(int id)
        {
            return await _roomRepository.GetAppointmentById(id);
        }

        public async Task CreateRoomAppointmentAsync(CreateRoomAppointmentDto createRoomAppointmentDto)
        {
			await _roomRepository.CreateRoomAppointmentAsync(createRoomAppointmentDto);
        }

        //     public Task AddRoomService(AddRoomServicesDto addRoomServicesDto)
        //     {
        //return _roomRepository.AddRoomServicesAsync(addRoomServicesDto); ;
        //     }

        //     public Task RemoveRoomServiceAsync(int roomId, int serviceId)
        //     {
        //         return _roomRepository.RemoveRoomServiceAsync(roomId, serviceId);
        //     }

        //     public Task<IEnumerable<RoomServiceResponseDto>> GetRoomServicesByRoomIdAsync(int roomId)
        //     {
        //return _roomRepository.GetRoomServicesByRoomIdAsync(roomId);
        //     }
    }
}
