using DTOs.Account;

namespace Repository.Interface
{
    public interface IAccountRepository
    {
        Task<AccountDto> GetAccountLoginByUsername(string username);
        Task<IEnumerable<AccountDto>> GetAllAsync();
        Task<AccountDto> GetAccountByEmail(string email);
        Task CreateAccount(AccountDto accountDto);
        Task UpdateAccount(AccountDto accountDto);
        Task<AccountDto> GetAccountById(int id);
        Task<CustomerViewAccount> GetAccountProfileById(int id); 
        Task<AccountDto> GetAccountWithHostelById(int id);

    }
}
