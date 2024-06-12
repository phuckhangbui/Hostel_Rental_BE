using AutoMapper;
using DTOs.Contract;
using DTOs.Enum;
using DTOs.RoomService;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public ContractService(
            IContractRepository contractRepository,
            IAccountRepository accountRepository,
            IMapper mapper,
            IRoomRepository roomRepository)
        {
            _contractRepository = contractRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _roomRepository = roomRepository;
        }

        public async Task ChangeContractStatus(int contractId, int status, DateTime datesigned)
        {
            var currentContract = await _contractRepository.GetContractDetailsByContractId(contractId);
            if (currentContract == null)
            {
                throw new ServiceException("Contract not found with given ID");
            }
            currentContract.Status = status;
            currentContract.DateSign = datesigned;

            await _contractRepository.UpdateContract(currentContract);
        }

        public async Task CreateContract(CreateContractDto contractDto)
        {
            try
            {
                int contractID = await _contractRepository.CreateContract(contractDto);
                CreateListContractMemberDto memberDto = new CreateListContractMemberDto();
                memberDto.ContractID = contractID;
                if (contractDto.ContractMember == null)
                {
                    var list = new List<CreateContractMemberDto>();
                    var account = await _accountRepository.GetAccountById(contractDto.OwnerAccountID);
                    CreateContractMemberDto member = new CreateContractMemberDto()
                    {
                        Name = account.Name,
                        Phone = account.Phone,
                        CitizenCard = account.CitizenCard,
                    };
                    list.Add(member);
                    memberDto.Members = list;
                }
                else
                {
                    memberDto.Members = contractDto.ContractMember;
                }
                await _roomRepository.UpdateAppointmentRoom(contractDto.RoomID);
                await _contractRepository.AddContractMember(memberDto);
                await _roomRepository.UpdateRoomStatus((int)contractDto.RoomID, (int)RoomEnum.Hiring);

                var serviceSelected = new List<RoomServiceUpdateDto>();
                foreach (var item in contractDto.RoomService)
                {
                    var service = new RoomServiceUpdateDto()
                    {
                        IsSelected = true,
                        RoomServiceId = item
                    };
                    serviceSelected.Add(service);
                }
                await _roomRepository.UpdateRoomServicesIsSelectStatusAsync((int)contractDto.RoomID, serviceSelected);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetContractDto> GetContractDetailByContractId(int contractId)
        {
            return await _contractRepository.GetContractDetailsByContractId(contractId);
        }

        public async Task<IEnumerable<GetContractDto>> GetContracts()
        {
            return await _contractRepository.GetContractsAsync();
        }


        public async Task<IEnumerable<GetContractDto>> GetContractsByOwnerId(int ownerId)
        {
            return await _contractRepository.GetContractByOwnerId(ownerId);
        }

        public async Task<IEnumerable<GetContractDto>> GetContractsByStudentId(int studentId)
        {
            return await _contractRepository.GetContractByStudentId(studentId);
        }

        public async Task UpdateContract(int id, UpdateContractDto contractDto)
        {
            await _contractRepository.UpdateContract(id, contractDto);
        }

        public async Task AddContractMember(CreateListContractMemberDto createListContractMemberDto)
        {
            await _contractRepository.AddContractMember(createListContractMemberDto);
        }

    }
}
