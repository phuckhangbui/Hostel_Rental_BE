

using AutoMapper;
using BusinessObject.Enum;
using BusinessObject.Models;
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

        public Task ChangeContractStatus(int contractId, int status)
        {
            throw new NotImplementedException();
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
            };
            await _contractRepository.CreateContract(contract);
        }

        public async Task<IEnumerable<GetContractDto>> GetContracts()
        {
            var contracts = await _contractRepository.GetContractsAsync();
            return _mapper.Map<IEnumerable<GetContractDto>>(contracts);
        }

        public async Task UpdateContract(ContractDto contractDto)
        {
            var currentContract = await _contractRepository.GetContractById(contractDto.ContractID);
            if (currentContract == null)
            {
                throw new ServiceException("Contract not found with given ID");
            }

            //if (contractDto.AccountID.HasValue)
            //{
            //    currentContract.AccountID = contractDto.AccountID;
            //}
            //if (contractDto.RoomID.HasValue)
            //{
            //    currentContract.RoomID = contractDto.RoomID;
            //}
            //if (!string.IsNullOrEmpty(contractDto.ContractTerm))
            //{
            //    currentContract.ContractTerm = contractDto.ContractTerm;
            //}
            //if (contractDto.CreatedDate.HasValue)
            //{
            //    currentContract.CreatedDate = contractDto.CreatedDate;
            //}
            //if (contractDto.DateStart.HasValue)
            //{
            //    currentContract.DateStart = contractDto.DateStart;
            //}
            //if (contractDto.DateEnd.HasValue)
            //{
            //    currentContract.DateEnd = contractDto.DateEnd;
            //}
            //if (contractDto.DateSign.HasValue)
            //{
            //    currentContract.DateSign = contractDto.DateSign;
            //}
            currentContract.Status = contractDto.Status;

            await _contractRepository.UpdateContract(currentContract);
        }

    }
}
