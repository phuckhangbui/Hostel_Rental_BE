using BusinessObject.Models;
using DTOs.Membership;

namespace Repository.Interface
{
    public interface IMemberShipRepository
    {
        Task<bool> CreateMemberShip(CreateMemberShipDto createMemberShipDto);
        Task UpdateMemberShip(MemberShip memberShip);
        Task<MemberShip> GetMembershipById(int memberShipID);
        Task<IEnumerable<GetMemberShipDto>> GetMembershipsActive();
        Task<IEnumerable<GetMemberShipDto>> GetMembershipExpire();
        Task<IEnumerable<GetMemberShipDto>> GetAllMemberships();
        Task<bool> UpdateMembershipStatus(MemberShip memberShip);
        Task<bool> CheckMembershipNameExist(string memberShipName);
    }
}
