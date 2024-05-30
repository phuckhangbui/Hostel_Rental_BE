using DTOs.Account;
using DTOs.AccountAuthentication;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountLoginByUsername(LoginDto loginDto);
        Task<IEnumerable<AccountViewDto>> GetAllAccounts();
        Task<AccountLoginDto> Login(EmailLoginDto login);
        Task RegisterEmail(EmailRegisterDto emailRegisterDto);
        Task ForgetPassword(EmailRegisterDto emailRegisterDto);
        Task<AccountLoginDto> ConfirmPassword(ConfirmPasswordDtos confirmPasswordDtos);
        Task<AccountLoginDto> LoginWithGoogle(LoginWithGoogleDto loginWithGoogle);
        Task<AccountLoginDto> RegisterWithGoogle(LoginWithGoogleDto loginWithGoogle);
        Task ActiveAccount(int idAccount);
        Task UnactiveAccount(int idAccount);
        Task<AccountViewDetail> GetAccountById(int id);
    }
}
