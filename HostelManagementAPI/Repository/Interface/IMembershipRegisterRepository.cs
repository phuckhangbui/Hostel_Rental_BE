using BusinessObject.Models;
using DTOs.Membership;
using DTOs.MemberShipRegisterTransaction;

namespace Repository.Interface
{
    public interface IMembershipRegisterRepository
    {
        Task<IEnumerable<ViewMemberShipDto>> GetAllMemberships();
        Task<MemberShipRegisterTransaction> GetDetailMemberShipRegister(int registerID);
    }
}
