using BusinessObject.Models;
using Dao;
using Repository.Interface;

namespace Repository.Implement
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDao _accountDao;

        public AccountRepository()
        {
            _accountDao = new AccountDao();
        }

        public async Task<Account> FirebaseTokenExisted(string firebaseToken)
        {
            return await _accountDao.FirebaseTokenExisted(firebaseToken);
        }

        public async Task<Account> getAccountLoginByUsername(string username)
        {
            return await _accountDao.getAccountLoginByUsername(username);
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            return _accountDao.updateObject(account);
        }
    }
}
