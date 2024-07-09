using DTOs.Contract;
using DTOs.Enum;
using DTOs.RoomService;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;
using Service.Mail;

namespace Service.Implement
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IHostelRepository _hostelRepository;
        private readonly IMailService _mailService;
        private readonly INotificationService _notificationService;

        public ContractService(
            IContractRepository contractRepository,
            IAccountRepository accountRepository,
            IRoomRepository roomRepository,
            IHostelRepository hostelRepository,
            IMailService mailService,
            INotificationService notificationService)
        {
            _contractRepository = contractRepository;
            _accountRepository = accountRepository;
            _roomRepository = roomRepository;
            _hostelRepository = hostelRepository;
            _mailService = mailService;
            _notificationService = notificationService;
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
                var listAccount = await _roomRepository.UpdateAppointmentRoom(contractDto.RoomID, (int)contractDto.StudentAccountID);
                await _contractRepository.AddContractMember(memberDto);
                var accountHiring = await _accountRepository.GetAccountById((int)contractDto.StudentAccountID);
                var accountEmailHiring = accountHiring.Email;
                var accountNameHiring = accountHiring.Name;
                var inf = await _hostelRepository.GetHostelInformation((int)contractDto.RoomID);


                _mailService.SendMail(SendMailUserHiring.SendMailWithUserHiringSuccess(accountEmailHiring, accountNameHiring, inf));

                _notificationService.SendMemberWhoGetNewContract(accountHiring.AccountId, accountHiring.FirebaseToken, accountHiring.Name, inf);

                foreach (var accountID in listAccount)
                {
                    var declineHiring = await _accountRepository.GetAccountById(accountID);

                    var EmailHiring = declineHiring.Email;
                    var NameHiring = declineHiring.Name;
                    _mailService.SendMail(SendMailUserHiring.SendEmailDeclineAppointment(EmailHiring, NameHiring, inf));

                    _notificationService.SendMembersWhoGetDeclineContract(declineHiring.AccountId, declineHiring.FirebaseToken, declineHiring.Name, inf);
                }
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


            }
            catch (Exception ex)
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
