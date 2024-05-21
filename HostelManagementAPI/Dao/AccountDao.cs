using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class AccountDAO : BaseDAO<Account>
    {
        private static AccountDAO instance = null;
        private readonly DataContext dataContext;

        private AccountDAO() 
        {
            dataContext = new DataContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
            }
        }

        public async Task<Account> FirebaseTokenExisted(string firebaseToken)
        {
            return await dataContext.Account.FirstOrDefaultAsync(x => x.FirebaseToken == firebaseToken);
        }

        public async Task<Account> getAccountLoginByUsername(string username)
        {
            return await dataContext.Account.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
