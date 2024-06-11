using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class ContractMemberDao
    {
        private static ContractMemberDao instance = null;
        private static readonly object instacelock = new object();

        private ContractMemberDao()
        {

        }

        public static ContractMemberDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContractMemberDao();
                }
                return instance;
            }
        }

        public async Task<IEnumerable<ContractMember>> GetContractMembersAsync()
        {
            var context = new DataContext();
            return await context.ContractMember.ToListAsync();
        }
    }
}
