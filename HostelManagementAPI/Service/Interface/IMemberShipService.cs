using DTOs.Membership;

namespace Service.Interface
{
    public interface IMemberShipService
    {
        Task CreateMemberShip(CreateMemberShipDto createMemberShipDto);
        Task UpdateMemberShip(UpdateMemberShipAdminDto updateMemberShipAdmin);
        Task<IEnumerable<GetMemberShipDto>> GetMembershipsActive();
        Task<IEnumerable<GetMemberShipDto>> GetMembershipsExpire();
        Task<IEnumerable<GetMemberShipDto>> GetAllMemberships();
        Task<GetMemberShipDto> GetDetailMemberShip(int packageID);
        Task<bool> DeactivateMembership(UpdateMembershipDto updateMembershipDto);
        Task<bool> ActivateMembership(UpdateMembershipDto updateMembershipDto);
        Task<bool> CheckMembershipNameExist(string memberShipName);
    }
}
