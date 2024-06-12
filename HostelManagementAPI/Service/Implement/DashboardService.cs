using DTOs.Dashboard;
using Repository.Interface;
using Service.Interface;

namespace Service.Implement
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IEnumerable<AccountEachMemberShipDtos>> GetAmountAccountEachMemberShip()
        {
            var memberships = await _dashboardRepository.GetAmountAccountEachMemberShip();
            return memberships;
        }

        public async Task<IEnumerable<AccountMonthDtos>> GetAmountAccountEachMonth(int year)
        {
            var profits = await _dashboardRepository.GetAmountAccountEachMonth(year);
            return profits;
        }

        public async Task<IEnumerable<TypeMonthDtos>> GetAmountProfitEachMonth(int year)
        {
            var profits = await _dashboardRepository.GetAmountProfitEachMonth(year);
            return profits;
        }

        public async Task<Dashboard> GetStatiticDashboar()
        {
            return await _dashboardRepository.GetStatiticDashboar();
        }
    }
}
