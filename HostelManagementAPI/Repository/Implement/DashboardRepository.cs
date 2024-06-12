using DAO;
using DTOs.Dashboard;
using Repository.Interface;

namespace Repository.Implement
{
    public class DashboardRepository : IDashboardRepository
    {
        public async Task<IEnumerable<AccountEachMemberShipDtos>> GetAmountAccountEachMemberShip()
        {
            return await MemberShipRegisterDao.Instance.GetAmountAccountEachMemberShip();
        }

        public async Task<IEnumerable<AccountMonthDtos>> GetAmountAccountEachMonth(int year)
        {
            return await AccountDAO.Instance.GetAmountAccountEachMonth(year);
        }

        public async Task<IEnumerable<TypeMonthDtos>> GetAmountProfitEachMonth(int year)
        {
            return await MemberShipRegisterDao.Instance.GetAmountProfitEachMonth(year);
        }

        public async Task<Dashboard> GetStatiticDashboar()
        {
            Dashboard dashboard = new Dashboard()
            {
                TotalAccount = AccountDAO.Instance.GetTotalAccountsInFlatform().Result.Count(),
                TotalHostel = HostelDao.Instance.GetAllHostelsTotalActiveAsync().Result.Count(),
                TotalPackage = MemberShipDao.Instance.GetAllPackagesTotalActiveAsync().Result.Count(),
                TotalMemberShip = MemberShipRegisterDao.Instance.GetAllMemberShipTotalActiveAsync().Result.Count(),
                TotalProfit = MemberShipRegisterDao.Instance.GetProfitTotalAsync()
            };
            return dashboard;
        }
    }
}
