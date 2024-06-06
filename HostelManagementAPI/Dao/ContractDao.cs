using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class ContractDao : BaseDAO<Contract>
    {
        private static ContractDao instance = null;
        private static readonly object instacelock = new object();

        private ContractDao()
        {

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
            var context = new DataContext();
            return await context.Contract.FirstOrDefaultAsync(c => c.ContractID == id);
        }

        public async Task<IEnumerable<Contract>> GetContractsAsync()
        {
            var context = new DataContext();
            return await context.Contract
                .Include(c => c.Members)
                .ToListAsync();
        }

        //public async Task<IEnumerable<Contract>> GetContractsByOwnerIDAsync(int ownerID)
        //{
        //    var context = new DataContext();
        //    return await context.Contract
        //        .Include(c => c.Room)
        //        .Include(c => c.OwnerAccount)
        //        .Include(c => c.StudentLeadAccount)
        //        .Include(c => c.ContractDetails)
        //            .ThenInclude(cd => cd.Service)
        //        .Where(c => c.OwnerAccountID == ownerID)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Contract>> GetContractsByStudentIDAsync(int studentID)
        //{
        //    var context = new DataContext();
        //    return await context.Contract
        //        .Include(c => c.Room)
        //        .Include(c => c.OwnerAccount)
        //        .Include(c => c.StudentLeadAccount)
        //        .Include(c => c.ContractDetails)
        //            .ThenInclude(cd => cd.Service)
        //        .Where(c => c.StudentAccountID == studentID)
        //        .ToListAsync();
        //}

        //public async Task<Contract> GetContractByContractIDAsync(int contractID)
        //{
        //    var context = new DataContext();
        //    return await context.Contract
        //        .Include(c => c.Room)
        //        .Include(c => c.OwnerAccount)
        //        .Include(c => c.StudentLeadAccount)
        //        .Include(c => c.ContractDetails)
        //            .ThenInclude(cd => cd.Service)
        //        .FirstOrDefaultAsync(c => c.ContractID == contractID);
        //}

    }
}
