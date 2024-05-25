using BusinessObject.Models;

namespace Repository.Interface
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountLoginByUsername(string username);
        Task<bool> UpdateAsync(Account account);
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account> GetAccountByEmail(string email);
        Task CreateAccount(Account account);
        Task UpdateAccount(Account account);
        Task<Account> GetAccountById(int id);
    }
}
