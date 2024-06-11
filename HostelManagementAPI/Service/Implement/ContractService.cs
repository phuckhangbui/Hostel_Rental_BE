using AutoMapper;
using DTOs.Contract;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;
using System.Diagnostics.Contracts;

namespace Service.Implement
{
    public class ContractService : IContractService
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

        //public async Task ChangeContractStatus(int contractId, int status)
        //{
        //    var currentContract = await _contractRepository.GetContractById(contractId);
        //    if (currentContract == null)
        //    {
        //        throw new ServiceException("Contract not found with given ID");
        //    }
        //    currentContract.Status = status;

        //    await _contractRepository.UpdateContract(contractId, mapContract);
        //}

        public async Task CreateContract(CreateContractDto contractDto)
        {
            var contract = contractDto;
            await _contractRepository.CreateContract(contract);
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



    }
}
