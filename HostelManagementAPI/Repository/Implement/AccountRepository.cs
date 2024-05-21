using BusinessObject.Models;
using DAO;
using Repository.Interface;

namespace Repository.Implement
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<Account> GetAccountLoginByUsername(string username)
        {
            var account = await AccountDAO.Instance.GetAccountLoginByUsername(username);
            return account;
        }

        public Task<IEnumerable<Account>> GetAllAsync()
        {
            return AccountDAO.Instance.GetAllAsync();
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            return await AccountDAO.Instance.UpdateAsync(account);
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await AccountDAO.Instance.GetAccountByEmail(email);
        }

        public async Task CreateAccount(Account account)
        {
            await AccountDAO.Instance.CreateAsync(account);
        }

        public async Task UpdateAccount(Account account)
        {
            await AccountDAO.Instance.UpdateAsync(account);
        }
    }
}
