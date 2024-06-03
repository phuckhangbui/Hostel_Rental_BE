﻿using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAO
{
    public class ContractDao: BaseDAO<Contract>
    {
        private static ContractDao instance = null;
        private readonly DataContext dataContext;

        private ContractDao()
        {
            dataContext = new DataContext();
        }

        public static ContractDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContractDao();
                }
                return instance;
            }
        }

        public async Task<Contract> GetContractById(int id)
        {
            return await dataContext.Contract.FirstOrDefaultAsync(c => c.ContractID == id);
        }

        public async Task<IEnumerable<Contract>> GetContractsAsync()
        {
            return await dataContext.Contract
                .Include(c => c.Room)
                .Include(c => c.OwnerAccount)
                .Include(c => c.StudentLeadAccount)
                .Include(c => c.ContractDetails)  // Include ContractDetails
                    .ThenInclude(cd => cd.Service)  // Include Service within ContractDetails
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetContractsByOwnerIDAsync(int ownerID)
        {
            return await dataContext.Contract
                .Include(c => c.Room)
                .Include(c => c.OwnerAccount)
                .Include(c => c.StudentLeadAccount)
                .Include(c => c.ContractDetails)
                    .ThenInclude(cd => cd.Service)
                .Where(c => c.OwnerAccountID == ownerID)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetContractsByStudentIDAsync(int studentID)
        {
            return await dataContext.Contract
                .Include(c => c.Room)
                .Include(c => c.OwnerAccount)
                .Include(c => c.StudentLeadAccount)
                .Include(c => c.ContractDetails)
                    .ThenInclude(cd => cd.Service)
                .Where(c => c.StudentAccountID == studentID)
                .ToListAsync();
        }

    }
}
