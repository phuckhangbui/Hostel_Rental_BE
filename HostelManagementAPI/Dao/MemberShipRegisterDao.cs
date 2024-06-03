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
            DataContext _dataContext = new DataContext();
            return await _dataContext.MembershipsRegisterTransaction
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
            var membersRegister = await dataContext.MembershipsRegisterTransaction.Include(z => z.MemberShip).Where(x => x.AccountID == accountID).OrderByDescending(x => x.DateRegister)
                .ToListAsync();

            return membersRegister;
        }

        public async Task<IEnumerable<MemberShipRegisterTransaction>> GetAllTransactionMembership()
        {
            var membersRegister = await dataContext.MembershipsRegisterTransaction.Include(z => z.OwnerAccount).OrderByDescending(x => x.DateRegister)
                .ToListAsync();

            return membersRegister;
        }
    }
}
