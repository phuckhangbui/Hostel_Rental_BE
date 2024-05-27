using BusinessObject.Models;
using DTOs.Membership;

namespace Repository.Interface
{
    public interface IMemberShipRepository
    {
        Task<bool> CreateMemberShip(MemberShip memberShip);
        MemberShip GetMembershipById(int memberShipID);
        Task<IEnumerable<GetMemberShipDto>> GetMembershipsActive();
        Task<IEnumerable<GetMemberShipDto>> GetMembershipExpire();
        Task<IEnumerable<GetMemberShipDto>> GetAllMemberships();
        Task<bool> UpdateMembershipStatus(MemberShip memberShip);
    }
}
