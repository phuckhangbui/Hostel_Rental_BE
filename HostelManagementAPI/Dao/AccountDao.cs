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

        public async Task<Account> GetAccountLoginByUsername(string username)
        {
            return await dataContext.Account.FirstOrDefaultAsync(x => x.Username == username);
        }

        public override async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await dataContext.Account.Where(x => x.RoleId != 1).ToListAsync();
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            return await dataContext.Account.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await dataContext.Account.FirstOrDefaultAsync(x => x.AccountID.Equals(id));
        }

    }
}
