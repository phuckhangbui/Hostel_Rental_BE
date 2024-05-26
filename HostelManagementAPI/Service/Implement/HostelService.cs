using AutoMapper;
using BusinessObject.Enum;
using BusinessObject.Models;
using DTOs.Hostel;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class HostelService : IHostelService
    {
        private readonly IHostelRepository _hostelRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public HostelService(
            IHostelRepository hostelRepository,
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _hostelRepository = hostelRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task ChangeHostelStatus(int hostelId, int status)
        {
            var currentHostel = await _hostelRepository.GetHostelById(hostelId);
            if (currentHostel == null)
            {
                throw new ServiceException("Hostel not found with this ID");
            }

            if (!Enum.IsDefined(typeof(HostelEnum), status))
            {
                throw new ServiceException("Invalid status value");
            }

            currentHostel.Status = status;
            await _hostelRepository.UpdateHostel(currentHostel);
        }

        public async Task CreateHostel(CreateHostelRequestDto createHostelRequestDto)
        {
            var ownerAccount = await _accountRepository.GetAccountById(createHostelRequestDto.AccountID);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }

            Hostel hostel = new Hostel
            {
                HostelName = createHostelRequestDto.HostelName,
                HostelAddress = createHostelRequestDto.HostelAddress,
                HostelDescription = createHostelRequestDto.HostelDescription,
                AccountID = createHostelRequestDto.AccountID,
                Status = (int)HostelEnum.Available,
            };

            await _hostelRepository.CreateHostel(hostel);
        }

        public async Task<IEnumerable<HostelListResponseDto>> GetHostels()
        {
            var hostels = await _hostelRepository.GetAllHostels();
            return _mapper.Map<IEnumerable<HostelListResponseDto>>(hostels);
        }

        public async Task<IEnumerable<HostelListResponseDto>> GetHostelsByOwner(int onwerId)
        {
            var ownerAccount = await _accountRepository.GetAccountWithHostelById(onwerId);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }
            if (ownerAccount.Hostels == null)
            {
                return null;
            }

            return _mapper.Map<IEnumerable<HostelListResponseDto>>(ownerAccount.Hostels);

        }


        public async Task UpdateHostel(UpdateHostelRequestDto updateHostelRequestDto)
        {
            var ownerAccount = await _accountRepository.GetAccountById(updateHostelRequestDto.AccountID);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }

            var currentHostel = await _hostelRepository.GetHostelById(updateHostelRequestDto.HostelId);
            if (currentHostel == null)
            {
                throw new ServiceException("Hostel not found with this ID");
            }

            if (currentHostel.AccountID != updateHostelRequestDto.AccountID)
            {
                throw new ServiceException("The hostel does not belong to the specified account.");
            }

            currentHostel.HostelName = updateHostelRequestDto.HostelName;
            currentHostel.HostelDescription = updateHostelRequestDto.HostelDescription;
            currentHostel.HostelAddress = updateHostelRequestDto.HostelAddress;

            await _hostelRepository.UpdateHostel(currentHostel);
        }
    }
}
