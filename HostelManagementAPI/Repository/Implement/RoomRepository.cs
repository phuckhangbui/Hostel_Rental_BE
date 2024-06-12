using AutoMapper;
using DTOs.Enum;
using BusinessObject.Models;
using DAO;
using DTOs.Room;
using Repository.Interface;
using DTOs.RoomAppointment;
using DTOs.RoomService;

namespace Repository.Implement
{
	public class RoomRepository : IRoomRepository
	{
		private readonly IMapper _mapper;

		public RoomRepository(IMapper mapper)
		{
			_mapper = mapper;
		}


		public async Task<int> CreateRoom(CreateRoomRequestDto createRoomRequestDto)
		{
			Room room = new Room
			{
				RoomName = createRoomRequestDto.RoomName,
				Capacity = createRoomRequestDto.Capacity,
				Lenght = createRoomRequestDto.Length,
				Width = createRoomRequestDto.Width,
				Description = createRoomRequestDto.Description,
				RoomFee = createRoomRequestDto.RoomFee,
				HostelID = createRoomRequestDto.HostelID,
				Area = createRoomRequestDto.Width * createRoomRequestDto.Length,
				Status = (int)RoomEnum.Available,
				RoomImages = new List<RoomImage>(),
				RoomServices = new List<RoomService>(),
			};

			foreach (var roomService in createRoomRequestDto.RoomServices)
			{
				var newRoomService = new RoomService
				{
					Status = (int)RoomServiceEnum.Active,
					TypeServiceId = roomService.TypeServiceId,
					Price = roomService.Price,
				};

				room.RoomServices.Add(newRoomService);
			}

			await RoomDao.Instance.CreateAsync(room);

			return room.RoomID;
		}

		public async Task<IEnumerable<RoomListResponseDto>> GetListRoomsByHostelId(int hostelId)
		{
			var rooms = await RoomDao.Instance.GetRoomListByHostelId(hostelId);

			return _mapper.Map<IEnumerable<RoomListResponseDto>>(rooms);
		}

		public async Task<Room> GetRoomById(int roomId)
		{
			return await RoomDao.Instance.GetRoomById(roomId);
		}

		public async Task<RoomDetailResponseDto> GetRoomDetailById(int roomId)
		{
			var room = await RoomDao.Instance.GetRoomDetailById(roomId);

			var roomDetailDto = _mapper.Map<RoomDetailResponseDto>(room);
			roomDetailDto.RoomImageUrls = room.RoomImages.Select(img => img.RoomUrl).ToList();

			return roomDetailDto;
		}

		public async Task UpdateRoom(int roomId, RoomRequestDto updateRoomRequestDto)
		{
			var room = await RoomDao.Instance.GetRoomById(roomId);

			room.RoomName = updateRoomRequestDto.RoomName;
			room.Capacity = updateRoomRequestDto.Capacity;
			room.Lenght = updateRoomRequestDto.Length;
			room.Width = updateRoomRequestDto.Width;
			room.Description = updateRoomRequestDto.Description;
			room.RoomFee = updateRoomRequestDto.RoomFee;

			await RoomDao.Instance.UpdateAsync(room);
		}

		public async Task UpdateRoomStatus(int roomId, int status)
		{
			var room = await RoomDao.Instance.GetRoomById(roomId);

			room.Status = status;

			await RoomDao.Instance.UpdateAsync(room);
		}

		public async Task UploadRoomImage(int roomId, List<string> imageUrls)
		{
			var room = await RoomDao.Instance.GetRoomById(roomId);

			if (room.RoomImages == null)
			{
				room.RoomImages = new List<RoomImage>();
			}

			foreach (var imageUrl in imageUrls)
			{
				var roomImage = new RoomImage
				{
					RoomUrl = imageUrl,
					RoomID = room.RoomID
				};

				room.RoomImages.Add(roomImage);
			}

			await RoomDao.Instance.UpdateAsync(room);
		}

		public async Task<IEnumerable<RoomOfHostelAdminView>> GetHostelDetailWithRoomAdminView(int hostelId)
		{
			var rooms = await RoomDao.Instance.GetRoomById(hostelId);
			return _mapper.Map<IEnumerable<RoomOfHostelAdminView>>(rooms);
		}

		public async Task<List<string>> GetRoomImagesByHostelId(int hostelId)
		{
			List<string> imageUrls = await RoomDao.Instance.GetRoomImagesByHostelId(hostelId);
			return imageUrls;
		}

        public async Task<IEnumerable<GetAppointmentDto>> GetRoomAppointmentsAsync()
        {
            var appointments = await RoomAppointmentDao.Instance.GetRoomAppointmentsAsync();
			return appointments;
        }

        public async Task<GetAppointmentDto> GetAppointmentById(int id)
        {
            var appointmentDetails = await RoomAppointmentDao.Instance.GetRoomAppointmentByIdAsync(id);
			return appointmentDetails;
        }

        public async Task CreateRoomAppointmentAsync(CreateRoomAppointmentDto createRoomAppointmentDto)
        {
			RoomAppointment room = new RoomAppointment
			{
				RoomId = createRoomAppointmentDto.RoomId,
				ViewerId = createRoomAppointmentDto.ViewerId,
				AppointmentTime = DateTime.Now,
				Status = 1,
			};
            var roomAppointment = _mapper.Map<RoomAppointment>(room);
			
			await RoomAppointmentDao.Instance.CreateAsync(roomAppointment);
        }

        public async Task UpdateRoomServicesIsSelectStatusAsync(int roomId, List<RoomServiceUpdateDto> roomServiceUpdates)
        {
            await RoomServiceDao.Instance.UpdateRoomServicesIsSelectStatusAsync(roomId, roomServiceUpdates);
        }

        public async Task<IEnumerable<RoomServiceResponseDto>> GetRoomServicesIsSelected(int roomId)
        {
			var selectedServices =  await RoomServiceDao.Instance.GetRoomServicesIsSelected(roomId);
			return _mapper.Map<IEnumerable<RoomServiceResponseDto>>(selectedServices);
        }
        public async Task<IEnumerable<RoomServiceView>> GetRoomServicesByRoom(int roomId)
        {
            var service = await RoomServiceDao.Instance.GetRoomServicesByRoom(roomId);
            return _mapper.Map<IEnumerable<RoomServiceView>>(service);
        }

        public async Task<GetAppointmentDto> GetApppointmentToCreateContract(int roomID)
        {
            var appointmentDetails = await RoomAppointmentDao.Instance.GetApppointmentToCreateContract(roomID);
            return appointmentDetails;
        }

        public async Task UpdateAppointmentRoom(int? roomID)
        {
            await RoomAppointmentDao.Instance.UpdateAppointmentRoom(roomID);
        }

        //public async Task AddRoomServicesAsync(AddRoomServicesDto roomServicesDto)
        //{
        //	var roomServices = roomServicesDto.ServiceId.Select(serviceId => new RoomService
        //	{
        //		RoomId = roomServicesDto.RoomId,
        //		ServiceId = serviceId,
        //		Status = roomServicesDto.Status
        //	});

        //	await RoomServiceDao.Instance.AddRoomServicesAsync(roomServices);
        //}

        //public async Task RemoveRoomServiceAsync(int roomId, int serviceId)
        //{
        //	await RoomServiceDao.Instance.RemoveRoomServiceAsync(roomId, serviceId);
        //}
    }
}
