using BusinessObject.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
namespace Dao
{
    public class AccountDao : BaseDao<Account>
    {
        private readonly HostelManagementDBContext _dbContext;
        public AccountDao() { _dbContext = new HostelManagementDBContext(); }

        public async Task<Account> getAccountLoginByUsername(string username)
        {
            return await _dbContext.Account.Include(p => p.Permissions).FirstOrDefaultAsync(x => x.Username == username);
        }
        public async Task<Account> FirebaseTokenExisted(string firebaseToken)
        {
            return await _dbContext.Account.FirstOrDefaultAsync(x => x.FirebaseToken == firebaseToken);
        }

        public override IEnumerable<Account> getListObject()
        {
            return _dbContext.Account.Include(p => p.Permissions).ToList();
        }
    }
}
