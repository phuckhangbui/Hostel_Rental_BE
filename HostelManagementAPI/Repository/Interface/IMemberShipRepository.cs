using DTOs.Membership;

namespace Repository.Interface
{
    public interface IMemberShipRepository
    {
        Task<bool> CreateMemberShip(CreateMemberShipDto createMemberShipDto);
        Task UpdateMemberShip(GetMemberShipDto memberShipDto);
        Task<GetMemberShipDto> GetMembershipById(int memberShipID);
        Task<IEnumerable<GetMemberShipDto>> GetMembershipsActive();
        Task<IEnumerable<GetMemberShipDto>> GetMembershipExpire();
        Task<IEnumerable<GetMemberShipDto>> GetAllMemberships();
        Task<bool> UpdateMembershipStatus(GetMemberShipDto memberShipDto);
        Task<bool> CheckMembershipNameExist(string memberShipName);
        Task<GetMemberShipDto> GetDetailMemberShip(int packageID);
    }
}
