using AutoMapper;
using BusinessObject.Models;
using DTOs.Complain;
using DTOs.Enum;
using DTOs.Hostel;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class ComplainService : IComplainService
    {
        private readonly IComplainRepository _complainRepository;
        private readonly INotificationService _notificationService;
        private readonly IRoomRepository _roomRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public ComplainService(IMapper mapper, IComplainRepository complainRepository, INotificationService notificationService, IRoomRepository roomRepository, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _complainRepository = complainRepository;
            _notificationService = notificationService;
            _roomRepository = roomRepository;
            _accountRepository = accountRepository;
        }

        public async Task CreateComplain(CreateComplainDto complainDto)
        {
            var complain = _mapper.Map<Complain>(complainDto);
            complain.AccountID = complainDto.AccountID;
            complain.DateComplain = DateTime.Now;
            complain.Status = (int)ComplainEnum.sent;

            var room = await _roomRepository.GetRoomById((int)complain.RoomID);
            var inf = new InformationHouse
            {
                HostelName = room.HostelName,
                Address = room.HostelAddress,
                RoomName = room.RoomName
            };

            var owner = await _accountRepository.GetAccountById((int)room.OwnerID);

            _notificationService.SendOwnerWhenMemberComplain(owner.AccountId, owner.FirebaseToken, owner.Name, inf, complain.ComplainText);

            await _complainRepository.CreateComplain(complain);
        }

        public async Task<ComplainDto> GetComplainById(int id)
        {
            var complain = await _complainRepository.GetComplainById(id);
            var displayComplain = _mapper.Map<ComplainDto>(complain);
            return displayComplain;
        }

        public async Task<IEnumerable<ComplainDto>> GetComplains()
        {
            return await _complainRepository.GetComplains();
        }

        public async Task<IEnumerable<ComplainDto>> GetComplainsByAccountCreator(int id)
        {
            var complains = await _complainRepository.GetComplains();
            var selectedComplains = complains.Where(c => c.AccountID == id);

            var displayComplains = _mapper.Map<IEnumerable<ComplainDto>>(selectedComplains);

            return displayComplains;
        }

        public async Task<IEnumerable<ComplainDto>> GetComplainsByRoom(int id)
        {
            var complains = await _complainRepository.GetComplains();
            var selectedComplains = complains.Where(c => c.RoomID == id);

            var displayComplains = _mapper.Map<IEnumerable<ComplainDto>>(selectedComplains);

            return displayComplains;
        }

        public async Task UpdateComplainStatus(UpdateComplainStatusDto updateComplainRequest)
        {
            var complain = await _complainRepository.GetComplainById(updateComplainRequest.ComplainId);
            if (complain == null)
            {
                throw new ServiceException("Complain not found");
            }

            complain.Status = (int)ComplainEnum.done;
            complain.DateUpdate = DateTime.Now;
            complain.ComplainResponse = updateComplainRequest.ComplainResponse;
            await _complainRepository.UpdateComplain(complain);

            var room = await _roomRepository.GetRoomById((int)complain.RoomID);
            var inf = new InformationHouse
            {
                HostelName = room.HostelName,
                Address = room.HostelAddress,
                RoomName = room.RoomName
            };

            var member = await _accountRepository.GetAccountById((int)complain.AccountID);

            _notificationService.SendMemberWhenOwnerReplyComplain(member.AccountId, member.FirebaseToken, member.Name, inf, complain.ComplainResponse);
        }
    }
}
