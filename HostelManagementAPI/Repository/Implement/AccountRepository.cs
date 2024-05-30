using AutoMapper;
using BusinessObject.Models;
using DAO;
using DTOs.Account;
using Repository.Interface;

namespace Repository.Implement
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;

        public AccountRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<AccountDto> GetAccountLoginByUsername(string username)
        {
            var account = await AccountDAO.Instance.GetAccountLoginByUsername(username);
            return _mapper.Map<AccountDto>(account);
        }

        public async Task<IEnumerable<AccountDto>> GetAllAsync()
        {
            var list = await AccountDAO.Instance.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountDto>>(list);
        }

        public async Task<AccountDto> GetAccountByEmail(string email)
        {
            var account = await AccountDAO.Instance.GetAccountByEmail(email);
            return _mapper.Map<AccountDto>(account);
        }

        public async Task CreateAccount(AccountDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);
            await AccountDAO.Instance.CreateAsync(account);
        }

        public async Task UpdateAccount(AccountDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);
            await AccountDAO.Instance.UpdateAsync(account);
        }

        public async Task<AccountDto> GetAccountById(int id)
        {
            var account = await AccountDAO.Instance.GetAccountById(id);
            return _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> GetAccountWithHostelById(int id)
        {
            var account = await AccountDAO.Instance.GetAccountWithHostelById(id);
            return _mapper.Map<AccountDto>(account);

        }

    }
}
