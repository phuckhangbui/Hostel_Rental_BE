using DTOs.MemberShipRegisterTransaction;

namespace Repository.Interface
{
    public interface IMembershipRegisterRepository
    {
        Task<IEnumerable<ViewHistoryMemberShipDtos>> GetAllMembershipPackageInAccount(int accountID);
    }
}
