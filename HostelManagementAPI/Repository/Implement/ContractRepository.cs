﻿using AutoMapper;
using BusinessObject.Models;
using DAO;
using DTOs.Contract;
using DTOs.Enum;
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

        public async Task<int> CreateContract(CreateContractDto contractDto)
        {
            var contract = new Contract
            {
                OwnerAccountID = contractDto.OwnerAccountID,
                StudentAccountID = contractDto.StudentAccountID,
                RoomID = contractDto.RoomID,
                CreatedDate = DateTime.Now,
                DateEnd = contractDto.DateEnd,
                DateStart = contractDto.DateStart,
                ContractTerm = contractDto.ContractTerm,
                Status = (int)ContractStatusEnum.pending,
                RoomFee = contractDto.RoomFee,
                DepositFee = contractDto.DepositFee,
                InitWaterNumber = contractDto.InitWater,
                InitElectricityNumber = contractDto.InitElec
            };

            await ContractDao.Instance.CreateAsync(contract);

            return contract.ContractID;
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

        public async Task AddContractMember(CreateListContractMemberDto createListContractMemberDto)
        {
            await ContractMemberDao.Instance.AddContractMembersAsync(createListContractMemberDto);
        }

        public async Task<GetContractDto> GetCurrentContractByRoom(int roomId)
        {
            var contract = await ContractDao.Instance.GetCurrentContractByRoom(roomId);

            return _mapper.Map<GetContractDto>(contract);
        }

        public async Task<IEnumerable<GetContractDto>> GetSignedContracs()
        {
            var contracs = await ContractDao.Instance.GetContractsAsync();

            var signedContracts = contracs.Where(c => c.Status == (int)ContractStatusEnum.signed).ToList();

            return _mapper.Map<IEnumerable<GetContractDto>>(signedContracts);
        }
    }
}
