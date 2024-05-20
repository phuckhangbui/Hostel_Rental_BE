using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class AccountDAO : BaseDAO<Account>
    {

        private AccountDAO() { }

        private static AccountDAO instance = null;
        private static readonly object instancelock = new Object();

        public static AccountDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<Account> FirebaseTokenExisted(string firebaseToken)
        {
            var context = new DataContext();
            return await context.Account.FirstOrDefaultAsync(x => x.FirebaseToken == firebaseToken);
        }

        public async Task<Account> getAccountLoginByUsername(string username)
        {
            var context = new DataContext();

            var list = await context.Account.ToListAsync();
            var account = list.FirstOrDefault(x => x.Username == username);
            return account;
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            var context = new DataContext();
            try
            {
                context.Account.Update(account);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
