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
                .Include(c => c.OwnerAccount)
                .Include(c => c.StudentLeadAccount)
                .Include(c => c.Room)
                    .ThenInclude(r => r.Hostel)  
                .Include(c => c.Room)
                    .ThenInclude(r => r.RoomServices)
                        .ThenInclude(t => t.TypeService)
                .Include(c => c.Members)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetContractsByOwnerIDAsync(int ownerID)
        {
            var context = new DataContext();
            return await context.Contract
                .Include(c => c.OwnerAccount)
                .Include(c => c.StudentLeadAccount)
                .Include(c => c.Room)
                    .ThenInclude(r => r.Hostel)  
                .Include(c => c.Room)
                    .ThenInclude(r => r.RoomServices)
                        .ThenInclude(t => t.TypeService)
                .Include(c => c.Members)
                .Where(c => c.OwnerAccountID == ownerID)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetContractsByStudentIDAsync(int studentID)
        {
            var context = new DataContext();
            return await context.Contract
                .Include(c => c.OwnerAccount)
                .Include(c => c.StudentLeadAccount)
                .Include(c => c.Room)
                    .ThenInclude(r => r.Hostel)
                .Include(c => c.Room)
                    .ThenInclude(r => r.RoomServices)
                        .ThenInclude(t => t.TypeService)
                .Include(c => c.Members)
                .Where(c => c.StudentAccountID == studentID)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<Contract> GetContractByContractIDAsync(int contractID)
        {
            var context = new DataContext();
            return await context.Contract
                .Include(c => c.OwnerAccount)
                .Include(c => c.StudentLeadAccount)
                .Include(c => c.Room)
                    .ThenInclude(r => r.Hostel)
                .Include(c => c.Room)
                    .ThenInclude(r => r.RoomServices)
                        .ThenInclude(t => t.TypeService)
                .Include(c => c.Members)
                .FirstOrDefaultAsync(c => c.ContractID == contractID);
        }

    }
}
