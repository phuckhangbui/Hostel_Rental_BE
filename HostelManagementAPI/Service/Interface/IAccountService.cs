using DTOs;
using DTOs.Account;
using DTOs.AccountAuthentication;
using DTOs.MemberShipRegisterTransaction;

namespace Service.Interface
{
    public interface IAccountService
    {
        //Task<AccountDto> GetAccountLoginByUsername(LoginDto loginDto);
        Task<IEnumerable<AccountViewDto>> GetAllAccounts();
        Task<AccountLoginDto> Login(EmailLoginDto login);
        Task RegisterEmail(EmailRegisterDto emailRegisterDto);
        Task ConfirmOtp(AccountConfirmDto accountConfirmDto);
        Task ResendRegisterOtp(string email);
        Task ForgetPassword(string email);
        Task<AccountLoginDto> ConfirmPassword(ConfirmPasswordDtos confirmPasswordDtos);
        Task<AccountLoginDto> LoginWithGoogle(LoginWithGoogleDto loginWithGoogle);
        Task<AccountLoginDto> RegisterWithGoogle(RegisterWithGoogleDto registerWithGoogle);
        Task ActiveAccount(int idAccount);
        Task UnactiveAccount(int idAccount);
        Task<AccountViewDetail> GetAccountById(int id);
        Task Logout(int accountId);
        Task<AccountLoginDto> RefreshToken(TokenApiDto tokenApiDto);
        Task<CustomerViewAccount> GetAccountProfileById(int id);
        Task<IEnumerable<ViewMemberShipDto>> GetAllMemberShip();
        Task<AccountMemberShipInformationDtos> GetDetailMemberShipRegisterInformation(int accountId);
    }
}
