using DTOs.Dashboard;

namespace Repository.Interface
{
    public interface IDashboardRepository
    {
        Task<Dashboard> GetStatiticDashboar();
        Task<IEnumerable<AccountEachMemberShipDtos>> GetAmountAccountEachMemberShip();
        Task<IEnumerable<TypeMonthDtos>> GetAmountProfitEachMonth(int year);
        Task<IEnumerable<AccountMonthDtos>> GetAmountAccountEachMonth(int year);
    }
}
