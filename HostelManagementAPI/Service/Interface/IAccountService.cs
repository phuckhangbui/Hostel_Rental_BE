using BusinessObject.Models;
using DTOs;
using DTOs.Account;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountLoginByUsername(LoginDto loginDto);
        Task<IEnumerable<AccountViewDto>> GetAllAccounts();
        Task<AccountDto> Login(EmailLoginDto login);
        Task RegisterEmail(EmailRegisterDto emailRegisterDto);
        Task ForgetPassword(EmailRegisterDto emailRegisterDto);
        Task<AccountDto> ConfirmPassword(ConfirmPasswordDtos confirmPasswordDtos);
        Task<AccountDto> LoginWithGoogle(LoginWithGoogleDto loginWithGoogle);
        Task<AccountDto> RegisterWithGoogle(LoginWithGoogleDto loginWithGoogle);
        Task ActiveAccount(int idAccount);
        Task UnactiveAccount(int idAccount);
        Task<AccountViewDetail> GetAccountById(int id);
    }
}
