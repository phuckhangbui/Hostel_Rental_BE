using BusinessObject.Models;
using DTOs.Dashboard;
using DTOs.MemberShipRegisterTransaction;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class MemberShipRegisterDao : BaseDAO<MemberShipRegisterTransaction>
    {
        private static MemberShipRegisterDao instance = null;
        private readonly DataContext dataContext;

        public MemberShipRegisterDao()
        {
            dataContext = new DataContext();
        }

        public static MemberShipRegisterDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MemberShipRegisterDao();
                }
                return instance;

            }
        }

        public async Task<IEnumerable<MemberShipRegisterTransaction>> GetAllMemberShipTotalActiveAsync()
        {
            return await dataContext.MembershipsRegisterTransaction.Where(x => x.Status == 0)
                .ToListAsync();
        }

        public double GetProfitTotalAsync()
        {
            var totalProfit = dataContext.MembershipsRegisterTransaction.Sum(x => x.PackageFee);
            return totalProfit;
        }

        public async Task<IEnumerable<AccountEachMemberShipDtos>> GetAmountAccountEachMemberShip()
        {
            var reals = dataContext.MembershipsRegisterTransaction.Include(x => x.MemberShip).Select(x => new AccountEachMemberShipDtos
            {
                MemberShipID = x.MemberShipID,
                MemberShipName = x.MemberShip.MemberShipName,
                NumberOfMember = dataContext.Membership.Where(y => y.MemberShipID == x.MemberShipID).Count(),
            });
            return reals;
        }

        public async Task<IEnumerable<TypeMonthDtos>> GetAmountProfitEachMonth()
        {
            var context = new DataContext();
            var utils = new Utils();
            var profit = context.MembershipsRegisterTransaction
        .GroupBy(x => x.DateRegister.Value.Month)
        .Select(group => new TypeMonthDtos
        {
            Month = new DateTime(1, group.Key, 1).ToString("MMMM"),
            FormattedNumberOfProfit = utils.FormatCurrency(group.Sum(x => x.PackageFee))
        });
            return profit;
        }

        public async Task<IEnumerable<ViewMemberShipDto>> GetAllMembership()
        {
            var membersRegister = await dataContext.MembershipsRegisterTransaction
                .Select(x => new ViewMemberShipDto
                {
                    MemberShipTransactionID = x.MemberShipTransactionID,
                    AccountID = (int)x.AccountID,
                    Email = x.OwnerAccount.Email,
                    MembershipName = x.MemberShip.MemberShipName,
                    DateRegister = x.DateRegister.Value,
                    DateExpire = x.DateExpire.Value,
                    Status = x.Status,
                })
                .ToListAsync();

            return membersRegister;
        }

        public async Task<MemberShipRegisterTransaction> GetDetailMemberShipRegister(int registerID)
        {
            return await dataContext.MembershipsRegisterTransaction.Include(x => x.OwnerAccount).Include(z => z.MemberShip).FirstOrDefaultAsync(y => y.MemberShipTransactionID == registerID);
        }
    }
}
