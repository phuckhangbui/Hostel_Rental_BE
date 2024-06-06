using AutoMapper;
using DTOs.Contract;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IAccountRepository _accountRepository;
        //private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ContractService(
            IContractRepository contractRepository,
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _contractRepository = contractRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task ChangeContractStatus(int contractId, int status)
        {
            var currentContract = await _contractRepository.GetContractById(contractId);
            if (currentContract == null)
            {
                throw new ServiceException("Contract not found with given ID");
            }
            currentContract.Status = status;

            await _contractRepository.UpdateContract(currentContract);
        }

        public async Task CreateContract(CreateContractDto contractDto)
        {
            var contract = contractDto;

            await _contractRepository.CreateContract(contract);
        }

        public async Task<GetContractDto> GetContractDetailByContractId(int contractId)
        {
            var contract = await _contractRepository.GetContractDetailsByContractId(contractId);
            if (contract == null)
            {
                throw new ServiceException("Contract not found with this ID");
            }
            return _mapper.Map<GetContractDto>(contract);
        }

        public async Task<IEnumerable<GetContractDto>> GetContracts()
        {
            var contracts = await _contractRepository.GetContractsAsync();
            return _mapper.Map<List<GetContractDto>>(contracts);

        }

        public async Task<IEnumerable<GetContractDto>> GetContractsByOwnerId(int ownerId)
        {
            var owner = await _accountRepository.GetAccountById(ownerId);
            if (owner == null)
            {
                throw new ServiceException("Owner not found with this ID");
            }
            var contracts = await _contractRepository.GetContractByOwnerId(ownerId);
            return _mapper.Map<List<GetContractDto>>(contracts);
        }

        public async Task<IEnumerable<GetContractDto>> GetContractsByStudentId(int studentId)
        {
            var student = await _accountRepository.GetAccountById(studentId);
            if (student == null)
            {
                throw new ServiceException("Student not found with this ID");
            }
            var contracts = await _contractRepository.GetContractByStudentId(studentId);
            return _mapper.Map<List<GetContractDto>>(contracts);
        }

        public async Task UpdateContract(UpdateContractDto contractDto)
        {
            var currentContract = await _contractRepository.GetContractById(contractDto.ContractID);
            if (currentContract == null)
            {
                throw new ServiceException("Contract not found with given ID");
            }
            currentContract.OwnerAccountID = contractDto.OwnerAccountId;
            currentContract.StudentAccountID = contractDto.StudentAccountID;
            currentContract.RoomID = contractDto.RoomID;
            currentContract.ContractTerm = contractDto.ContractTerm;
            currentContract.DateEnd = DateTime.Parse(contractDto.DateEnd);
            currentContract.DateSign = DateTime.Parse(contractDto.DateSign);
            currentContract.Status = contractDto.Status;
            currentContract.RoomFee = contractDto.RoomFee;
            currentContract.DepositFee = contractDto.DepositFee;

            await _contractRepository.UpdateContract(currentContract);
        }



    }
}
