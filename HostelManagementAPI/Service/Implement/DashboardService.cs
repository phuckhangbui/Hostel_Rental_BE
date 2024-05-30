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

        public async Task<IEnumerable<TypeMonthDtos>> GetAmountProfitEachMonth()
        {
            var profits = await _dashboardRepository.GetAmountProfitEachMonth();
            return profits;
        }

        public async Task<Dashboard> GetStatiticDashboar()
        {
            return await _dashboardRepository.GetStatiticDashboar();
        }
    }
}
