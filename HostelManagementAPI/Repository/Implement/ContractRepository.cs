using AutoMapper;
using BusinessObject.Models;
using DAO;
using DTOs.Contract;
using Repository.Interface;

namespace Repository.Implement
{

    public class ContractRepository : IContractRepository
    {
        private readonly IMapper _mapper;

        public ContractRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<bool> CreateContract(CreateContractDto contractDto)
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
                Status = 0,
                RoomFee = contractDto.RoomFee,
                DepositFee = contractDto.DepositFee,
            };

            await ContractDao.Instance.CreateAsync(contract);

            return true;
        }

        public async Task<IEnumerable<GetContractDto>> GetContractsAsync()
        {
            var contract = await ContractDao.Instance.GetContractsAsync();

            return _mapper.Map<IEnumerable<GetContractDto>>(contract);
        }

        public async Task<GetContractDto> GetContractById(int id)
        {
            var contract = await ContractDao.Instance.GetContractById(id);

            return _mapper.Map<GetContractDto>(contract);
        }

        public async Task UpdateContract(int id, UpdateContractDto contractDto)
        {
            var currentContract = await ContractDao.Instance.GetContractById(id);

            currentContract.StudentAccountID = contractDto.StudentAccountID;
            currentContract.RoomID = contractDto.RoomID;
            currentContract.ContractTerm = contractDto.ContractTerm;
            currentContract.DateEnd = DateTime.Parse(contractDto.DateEnd);
            currentContract.DateSign = DateTime.Parse(contractDto.DateSign);
            currentContract.Status = contractDto.Status;
            currentContract.RoomFee = contractDto.RoomFee;
            currentContract.DepositFee = contractDto.DepositFee;

            await ContractDao.Instance.UpdateAsync(currentContract);
        }

        public async Task<IEnumerable<GetContractDto>> GetContractByOwnerId(int ownerId)
        {
            var contract = await ContractDao.Instance.GetContractsByOwnerIDAsync(ownerId);
            return _mapper.Map<IEnumerable<GetContractDto>>(contract);
        }

        public async Task<IEnumerable<GetContractDto>> GetContractByStudentId(int studentId)
        {
            var contract = await ContractDao.Instance.GetContractsByStudentIDAsync(studentId);
            return _mapper.Map<IEnumerable<GetContractDto>>(contract);
        }

        public async Task<GetContractDto> GetContractDetailsByContractId(int contractId)
        {
            var contract = await ContractDao.Instance.GetContractByContractIDAsync(contractId);
            return _mapper.Map<GetContractDto>(contract);
        }
        public async Task UpdateContract(GetContractDto getContractDto)
        {
            var contract = _mapper.Map<Contract>(getContractDto);
            await ContractDao.Instance.UpdateAsync(contract);
        }

    }
}
