using BusinessObject.Models;
using DTOs;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountLoginByUsername(LoginDto loginDto);
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<AccountDto> Login(EmailLoginDto login);
        Task RegisterEmail(EmailRegisterDto emailRegisterDto);
        Task ForgetPassword(EmailRegisterDto emailRegisterDto);
        Task<AccountDto> ConfirmPassword(ConfirmPasswordDtos confirmPasswordDtos);
        Task<AccountDto> LoginWithGoogle(LoginWithGoogleDto loginWithGoogle);
        Task<AccountDto> RegisterWithGoogle(LoginWithGoogleDto loginWithGoogle);
    }
}
