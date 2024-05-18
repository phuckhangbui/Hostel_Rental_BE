using BusinessObject.Models;

namespace Repository.Interface
{
    public interface IAccountRepository
    {
        Task<Account> getAccountLoginByUsername(string username);
        Task<Account> FirebaseTokenExisted(string firebaseToken);
        Task<bool> UpdateAsync(Account account);
    }
}
