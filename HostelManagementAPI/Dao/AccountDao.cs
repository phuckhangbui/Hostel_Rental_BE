using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class AccountDAO : BaseDAO<Account>
    {
        private static AccountDAO instance = null;
        private static readonly object instacelock = new object();

        private AccountDAO()
        {

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
            var context = new DataContext();
            return await context.Account.FirstOrDefaultAsync(x => x.Username == username);
        }

        public override async Task<IEnumerable<Account>> GetAllAsync()
        {
            var context = new DataContext();
            return await context.Account.Where(x => x.RoleId != 1).OrderByDescending(x => x.AccountID).ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetTotalAccountsInFlatform()
        {
            var context = new DataContext();
            return await context.Account.Where(x => x.RoleId != 1 && x.Status == 0).ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetAllMemberShip()
        {
            var context = new DataContext();
            return await context.Account.Where(x => x.RoleId == 2).ToListAsync();
        }

        public async Task<Account> GetAccountByEmail(string email)
        {
            var context = new DataContext();
            return await context.Account.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<Account> GetAccountById(int id)
        {
            var context = new DataContext();
            return await context.Account.FirstOrDefaultAsync(x => x.AccountID.Equals(id));
        }

        public async Task<Account> GetAccountWithHostelById(int id)
        {
            var context = new DataContext();
            return await context.Account.Include(x => x.Hostels).FirstOrDefaultAsync(x => x.AccountID == id);
        }

    }
}
