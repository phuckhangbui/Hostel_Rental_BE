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
        Task<MemberShipRegisterTransactionDto> ExtendMembership(RegisterMemberShipDto registerMemberShipDto);
        Task<MemberShipRegisterTransactionDto> UpdateMembership(MembershipUpdatePackageDto membershipUpdatePackageDto);
        Task<MemberShipRegisterTransactionDto> ConfirmTransaction(VnPayReturnUrlDto vnPayReturnUrlDto, int accountId);
        Task<MemberShipRegisterTransactionDto> GetCurrentActiveMembership(int accountId);
    }
}
