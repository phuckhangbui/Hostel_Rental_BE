//using AutoMapper;
//using DTOs.Enum;
//using BusinessObject.Models;
//using DAO;
//using DTOs.Hostel;
//using Repository.Interface;
//using DTOs.HostelService;

//namespace Repository.Implement
//{
//    public class HostelRepository : IHostelRepository
//	{
//		private readonly IMapper _mapper;

//        public HostelRepository(IMapper mapper)
//        {
//            _mapper = mapper;
//        }

//        public async Task<int> CreateHostel(CreateHostelRequestDto createHostelRequestDto)
//		{
//			Hostel hostel = new Hostel
//			{
//				HostelName = createHostelRequestDto.HostelName,
//				HostelAddress = createHostelRequestDto.HostelAddress,
//				HostelDescription = createHostelRequestDto.HostelDescription,
//				AccountID = createHostelRequestDto.AccountID,
//				Status = (int)HostelEnum.Available,
//			};

//			await HostelDao.Instance.CreateAsync(hostel);

//			return hostel.HostelID;
//		}

//		public async Task<IEnumerable<HostelResponseDto>> GetAllHostels()
//		{
//			var hostels = await HostelDao.Instance.GetAllHostelsAsync();
//			return _mapper.Map<IEnumerable<HostelResponseDto>>(hostels);
//		}

//		public async Task<HostelResponseDto> GetHostelDetailById(int id)
//		{
//			var hostel = await HostelDao.Instance.GetHostelById(id);

//			return _mapper.Map<HostelResponseDto>(hostel);
//		}

//		public async Task<IEnumerable<HostelResponseDto>> GetOwnerHostels(int ownerId)
//		{
//			var hostels = await HostelDao.Instance.GetAllHostelsAsync();
//			hostels = hostels.Where(h => h.AccountID == ownerId).OrderByDescending(h => h.HostelID);
//			return _mapper.Map<IEnumerable<HostelResponseDto>>(hostels);
//		}

//		public async Task UpdateHostel(int hostelId, UpdateHostelRequestDto updateHostelRequestDto)
//		{
//			var currentHostel = await HostelDao.Instance.GetHostelById(hostelId);

//			currentHostel.HostelName = updateHostelRequestDto.HostelName;
//			currentHostel.HostelDescription = updateHostelRequestDto.HostelDescription;
//			currentHostel.HostelAddress = updateHostelRequestDto.HostelAddress;

//			await HostelDao.Instance.UpdateAsync(currentHostel);
//		}

//		public async Task UpdateHostelImage(int hostelId, string imageUrl)
//		{
//			var currentHostel = await HostelDao.Instance.GetHostelById(hostelId);

//			currentHostel.Thumbnail = imageUrl;

//			await HostelDao.Instance.UpdateAsync(currentHostel);
//		}

//		public async Task UpdateHostelStatus(int hostelId, int status)
//		{
//			var currentHostel = await HostelDao.Instance.GetHostelById(hostelId);

//			currentHostel.Status = status;

//			await HostelDao.Instance.UpdateAsync(currentHostel);
//		}

//        public async Task<HostelDetailAdminView> GetHostelDetailAdminView(int id)
//        {
//            var hostel = await HostelDao.Instance.GetHostelById(id);

//            return _mapper.Map<HostelDetailAdminView>(hostel);
//        }

//        public async Task<IEnumerable<HostelsAdminView>> GetHostelsAdminView()
//        {
//            var hostels = await HostelDao.Instance.GetAllHostelsAsync();

//            return _mapper.Map<IEnumerable<HostelsAdminView>>(hostels);
//        }

//        public async Task<IEnumerable<HostelServiceResponseDto>> GetHostelServices(int id)
//        {
//            var hostelSerices = await HostelDao.Instance.GetHostelServicesByHostelId(id);

//			return _mapper.Map<IEnumerable<HostelServiceResponseDto>>(hostelSerices);
//        }

//        public async Task AddHostelServices(int hostelId, HostelServiceRequestDto hostelServiceRequestDto)
//        {
//            var hostelServices = hostelServiceRequestDto.ServiceId.Select(serviceId => new HostelService
//            {
//                HostelId = hostelId,
//                ServiceId = serviceId,
//                Status = (int)HostelServiceEnum.Active,
//            });

//			await HostelDao.Instance.AddHostelServices(hostelServices);
//        }
//    }
//}
