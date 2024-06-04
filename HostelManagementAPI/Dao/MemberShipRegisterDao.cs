using BusinessObject.Models;
using DTOs.Dashboard;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class MemberShipRegisterDao : BaseDAO<MemberShipRegisterTransaction>
    {
        private static MemberShipRegisterDao instance = null;
        private static readonly object instacelock = new object();

        public MemberShipRegisterDao()
        {

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
            var context = new DataContext();
            return await context.MembershipsRegisterTransaction
                .ToListAsync();
        }

        public double GetProfitTotalAsync()
        {
            var context = new DataContext();
            var totalProfit = context.MembershipsRegisterTransaction.Sum(x => x.PackageFee);
            return totalProfit;
        }

        public async Task<IEnumerable<AccountEachMemberShipDtos>> GetAmountAccountEachMemberShip()
        {
            var context = new DataContext();
            var reals = context.MembershipsRegisterTransaction.Include(x => x.MemberShip).Select(x => new AccountEachMemberShipDtos
            {
                MemberShipID = x.MemberShipID,
                MemberShipName = x.MemberShip.MemberShipName,
                NumberOfMember = context.Membership.Where(y => y.MemberShipID == x.MemberShipID).Count(),
            });
            return reals;
        }

        public async Task<IEnumerable<TypeMonthDtos>> GetAmountProfitEachMonth()
        {
            var context = new DataContext();
            var utils = new Utils();
            DateTime date = DateTime.Now;
            int year = date.Year;
            var profit = context.MembershipsRegisterTransaction.Where(x => x.DateRegister.Value.Year == year)
        .GroupBy(x => x.DateRegister.Value.Month)
        .Select(group => new TypeMonthDtos
        {
            Month = new DateTime(1, group.Key, 1).ToString("MMMM"),
            FormattedNumberOfProfit = utils.FormatCurrency(group.Sum(x => x.PackageFee))
        });
            return profit;
        }

        public async Task<IEnumerable<MemberShipRegisterTransaction>> GetAllMembershipPackageInAccount(int accountID)
        {
            var context = new DataContext();
            var membersRegister = await context.MembershipsRegisterTransaction.Include(z => z.MemberShip).Where(x => x.AccountID == accountID).OrderByDescending(x => x.DateRegister)
                .ToListAsync();

            return membersRegister;
        }

        public async Task<IEnumerable<MemberShipRegisterTransaction>> GetAllTransactionMembership()
        {
            var context = new DataContext();
            var membersRegister = await context.MembershipsRegisterTransaction.Include(z => z.OwnerAccount).OrderByDescending(x => x.DateRegister)
                .ToListAsync();

            return membersRegister;
        }
    }
}
