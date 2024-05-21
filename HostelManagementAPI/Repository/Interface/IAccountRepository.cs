using BusinessObject.Models;

namespace Repository.Interface
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountLoginByUsername(string username);
        Task<bool> UpdateAsync(Account account);
        Task<IEnumerable<Account>> GetAllAsync();
    }
}
