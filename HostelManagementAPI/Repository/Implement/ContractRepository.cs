using BusinessObject.Models;
using DAO;
using DTOs.Contract;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{

    public class ContractRepository: IContractRepository
    {
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
                ContractDetails = new List<ContractDetail>()
            };

            foreach (var detailDto in contractDto.ContractDetails)
            {
                var contractDetail = new ContractDetail
                {
                    ServiceID = detailDto.ServiceID,
                };
                contract.ContractDetails.Add(contractDetail);
            }

            await ContractDao.Instance.CreateAsync(contract);

            return true;
        }

        public async Task<IEnumerable<Contract>> GetContractsAsync()
        {
            return await ContractDao.Instance.GetContractsAsync();
        }

        public async Task<Contract> GetContractById(int id)
        {
            return await ContractDao.Instance.GetContractById(id);
        }

        public async Task UpdateContract(Contract contract)
        {
            await ContractDao.Instance.UpdateAsync(contract);
        }

        public async Task<IEnumerable<Contract>> GetContractByOwnerId(int ownerId)
        {
            return await ContractDao.Instance.GetContractsByOwnerIDAsync(ownerId);
        }

        public async Task<IEnumerable<Contract>> GetContractByStudentId(int studentId)
        {
            return await ContractDao.Instance.GetContractsByStudentIDAsync(studentId);
        }

        public async Task<Contract> GetContractDetailsByContractId(int contractId)
        {
            return await ContractDao.Instance.GetContractByContractIDAsync(contractId);
        }
    }
}
