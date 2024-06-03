using DTOs.MemberShipRegisterTransaction;

namespace Service.Interface
{
    public interface IMembershipRegisterService
    {
        Task<IEnumerable<ViewHistoryMemberShipDtos>> GetAllMembershipPackageInAccount(int accountID);
        Task<IEnumerable<ViewTransactionMembership>> GetAllTransactionInAdmin();
    }
}
