using BusinessObject.Dtos;
using BusinessObject.Models;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<UserDto> GetAccountLoginByUsername(LoginDto loginDto);
        Task<IEnumerable<Account>> GetAllAccounts();
    }
}
