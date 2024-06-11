using DTOs;
using DTOs.Membership;
using DTOs.MemberShipRegisterTransaction;

namespace Service.Interface
{
    public interface IMembershipRegisterService
    {
        Task<IEnumerable<ViewHistoryMemberShipDtos>> GetAllMembershipPackageInAccount(int accountID);
        Task<IEnumerable<ViewTransactionMembership>> GetAllTransactionInAdmin();
        Task<MemberShipRegisterTransactionDto> RegisterMembership(RegisterMemberShipDto registerMemberShipDto);
        Task<MemberShipRegisterTransactionDto> ConfirmTransaction(VnPayReturnUrlDto vnPayReturnUrlDto);
    }
}
