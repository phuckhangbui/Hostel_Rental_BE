using DTOs.Membership;

namespace Service.Interface
{
    public interface IMemberShipService
    {
        Task CreateMemberShip(CreateMemberShipDto createMemberShipDto);
        Task<IEnumerable<GetMemberShipDto>> GetMembershipsActive();
        Task<IEnumerable<GetMemberShipDto>> GetMembershipsExpire();
        Task<IEnumerable<GetMemberShipDto>> GetAllMemberships();   
        Task<bool> DeactivateMembership(UpdateMembershipDto updateMembershipDto);
        Task<bool> ActivateMembership(UpdateMembershipDto updateMembershipDto);
    }
}
