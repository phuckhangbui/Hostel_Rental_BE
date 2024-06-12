using DTOs.Dashboard;

namespace Service.Interface
{
    public interface IDashboardService
    {
        Task<Dashboard> GetStatiticDashboar();
        Task<IEnumerable<AccountEachMemberShipDtos>> GetAmountAccountEachMemberShip();
        Task<IEnumerable<TypeMonthDtos>> GetAmountProfitEachMonth(int year);
        Task<IEnumerable<AccountMonthDtos>> GetAmountAccountEachMonth(int year);
    }
}
