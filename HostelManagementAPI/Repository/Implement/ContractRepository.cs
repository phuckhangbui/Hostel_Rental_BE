using BusinessObject.Models;
using DAO;
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
        public async Task<bool> CreateContract(Contract contract)
        {
            return await ContractDao.Instance.CreateAsync(contract);
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
    }
}
