using BusinessObject.Models;
using DTOs;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<UserDto> GetAccountLoginByUsername(LoginDto loginDto);
        Task<IEnumerable<Account>> GetAllAccounts();
    }
}
