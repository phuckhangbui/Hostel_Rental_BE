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

        public async Task<Account> FirebaseTokenExisted(string firebaseToken)
        {
            return await AccountDAO.Instance.FirebaseTokenExisted(firebaseToken);
        }

        public async Task<Account> getAccountLoginByUsername(string username)
        {
            var account = await AccountDAO.Instance.getAccountLoginByUsername(username);
            return account;
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            return await AccountDAO.Instance.UpdateAsync(account);
        }
    }
}
