using BusinessObject.Models;
using DTOs.Enum;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class MemberShipDao : BaseDAO<MemberShip>
    {
        private static MemberShipDao instance = null;
        private static readonly object instacelock = new object();
        public MemberShipDao()
        {
        }

        public static MemberShipDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MemberShipDao();
                }
                return instance;

            }
        }

        public async Task<MemberShip> GetMemberShipById(int id)
        {
            var context = new DataContext();
            MemberShip memberShip = null;
            memberShip = context.Membership.FirstOrDefault(x => x.MemberShipID == id);
            return memberShip;
        }

        public async Task<IEnumerable<MemberShip>> GetAllPackagesTotalActiveAsync()
        {
            var context = new DataContext();
            return await context.Membership.Where(x => x.Status == 0)
                .ToListAsync();
        }

        public async Task<MemberShipRegisterTransaction> GetMembershipRegisterStatusDone(int accountId)
        {
            var context = new DataContext();

            return await context.MembershipsRegisterTransaction
                .Include(m => m.MemberShip)
                .Where(m => m.AccountID == accountId && m.Status == (int)MembershipRegisterEnum.current)
                .OrderByDescending(m => m.DateRegister)
                .FirstOrDefaultAsync();
        }
    }
}
