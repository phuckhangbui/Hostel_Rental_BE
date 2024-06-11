using BusinessObject.Models;
using DTOs.Contract;
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

        public async Task AddContractMembersAsync(CreateListContractMemberDto createListContractMemberDto)
        {
            var context = new DataContext();

            foreach (var memberDto in createListContractMemberDto.Members)
            {
                var member = new ContractMember
                {
                    Name = memberDto.Name,
                    Phone = memberDto.Phone,
                    CitizenCard = memberDto.CitizenCard,
                    ContractID = createListContractMemberDto.ContractID
                };

                context.ContractMember.Add(member);
            }

            await context.SaveChangesAsync();
        }
    }
}
