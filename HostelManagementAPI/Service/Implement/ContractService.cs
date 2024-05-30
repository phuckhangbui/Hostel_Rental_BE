

using AutoMapper;
using BusinessObject.Enum;
using BusinessObject.Models;
using DAO;
using DTOs.Contract;
using DTOs.Hostel;
using Repository.Implement;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class ContractService: IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IAccountRepository _accountRepository;
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
            var contract = new Contract
            {
                OwnerAccountID = contractDto.OwnerAccountId,
                StudentAccountID = contractDto.StudentAccountID,
                RoomID = contractDto.RoomID,
                CreatedDate = DateTime.Now,
                DateEnd = contractDto.DateEnd,
                DateSign = contractDto.DateSign,
                DateStart = contractDto.DateStart,
                ContractTerm = contractDto.ContractTerm,
                Status = contractDto.Status,
                RoomFee = contractDto.RoomFee,
                DepositFee = contractDto.DepositFee,
            };
            await _contractRepository.CreateContract(contract);
        }

        public async Task<IEnumerable<GetContractDto>> GetContracts()
        {
            var contracts = await _contractRepository.GetContractsAsync();
            return _mapper.Map<IEnumerable<GetContractDto>>(contracts);
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
