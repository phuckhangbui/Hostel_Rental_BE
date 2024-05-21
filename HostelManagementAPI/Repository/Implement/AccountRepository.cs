using BusinessObject.Models;
using DAO;
using Repository.Interface;

namespace Repository.Implement
{
    //public class AccountRepository : IAccountRepository
    //{
    //    private readonly AccountDao _accountDao;

    //    public AccountRepository()
    //    {
    //        _accountDao = new AccountDao();
    //    }

    //    public async Task<Account> FirebaseTokenExisted(string firebaseToken)
    //    {
    //        return await _accountDao.FirebaseTokenExisted(firebaseToken);
    //    }

    //    public async Task<Account> getAccountLoginByUsername(string username)
    //    {
    //        return await _accountDao.getAccountLoginByUsername(username);
    //    }

    //    public async Task<bool> UpdateAsync(Account account)
    //    {
    //        return _accountDao.updateObject(account);
    //    }
    //}

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
    }
}
