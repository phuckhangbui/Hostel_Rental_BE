using DTOs.Membership;
using DTOs.MemberShipRegisterTransaction;

namespace Service.Interface
{
    public interface IMembershipRegisterService
    {
        Task<IEnumerable<ViewMemberShipDto>> GetAllMemberships();
    }
}
