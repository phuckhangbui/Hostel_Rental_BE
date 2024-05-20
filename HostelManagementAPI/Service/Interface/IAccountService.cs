using BusinessObject.Dtos;
using BusinessObject.Models;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<Account> GetAccountAsync(string username);
        Task<UserDto> getAccountLogin(LoginDto loginDto);
        Task<Account> FirebaseTokenExisted(string firebaseToken);
    }
}
