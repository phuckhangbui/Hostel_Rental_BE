using BusinessObject.Dtos;
using BusinessObject.Models;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<Account> GetAccountAsync(string username);
        Task<UserDto> GetAccountLogin(LoginDto loginDto);
        Task<Account> FirebaseTokenExisted(string firebaseToken);
        Task<IEnumerable<Account>> GetAllAccounts();
    }
}
