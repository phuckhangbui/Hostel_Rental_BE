﻿using DAO;
using DTOs.Room;
using DTOs.RoomAppointment;
using DTOs.RoomService;
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
		Task UpdateRoom(int roomId, RoomRequestDto updateRoomRequestDto);
		Task<List<string>> GetRoomImagesByHostelId(int hostelId);
        Task<IEnumerable<GetAppointmentDto>> GetRoomAppointmentsAsync();
        Task<GetAppointmentDto> GetAppointmentById(int id);
        Task CreateRoomAppointmentAsync(CreateRoomAppointmentDto createRoomAppointmentDto);
        Task UpdateRoomServicesIsSelectStatusAsync(int roomId, List<RoomServiceUpdateDto> roomServiceUpdates);

        //Task AddRoomService(AddRoomServicesDto addRoomServicesDto);
        //Task RemoveRoomServiceAsync(int roomId, int serviceId);
        //      Task<IEnumerable<RoomServiceResponseDto>> GetRoomServicesByRoomIdAsync(int roomId);
        Task<bool> UpdateRoomStatus(int roomId, int status);
    }
}
