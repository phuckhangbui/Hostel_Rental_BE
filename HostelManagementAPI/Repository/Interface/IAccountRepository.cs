using DTOs.Account;
using DTOs.MemberShipRegisterTransaction;

namespace Repository.Interface
{
    public interface IAccountRepository
    {
        Task<AccountDto> FirebaseTokenExisted(string firebaseToken);
        Task<IEnumerable<AccountDto>> GetAllAsync();
        Task<AccountDto> GetAccountByEmail(string email);
        Task CreateAccount(AccountDto accountDto);
        Task UpdateAccount(AccountDto accountDto);
        Task RemoveAccount(AccountDto accountDto);
        Task<AccountDto> GetAccountById(int id);
        Task<CustomerViewAccount> GetAccountProfileById(int id);
        Task<AccountDto> GetAccountWithHostelById(int id);
        Task<IEnumerable<ViewMemberShipDto>> GetAllMemberShip();
        Task<AccountMemberShipInformationDtos> GetDetailMemberShipRegisterInformation(int accountid);
        Task<ProfileDto> GetProfileAccount(int accountID);
        Task UpdateOwnerProfile(AccountUpdate accountUpdate);
        Task RemoveAccountInDB(int accountID);
    }
}
