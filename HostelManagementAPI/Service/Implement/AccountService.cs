using BusinessObject.Models;
using DTOs;
using Microsoft.IdentityModel.Tokens;
using Repository.Implement;
using Repository.Interface;
using Service.Interface;
using System.Security.Cryptography;
using System.Text;


namespace Service.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;
        public AccountService(IAccountRepository accountRepository, ITokenService tokenService) { _accountRepository = accountRepository; _tokenService = tokenService; }

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }

        public async Task<UserDto> GetAccountLoginByUsername(LoginDto loginDto)
        {
            Account account = await _accountRepository.GetAccountLoginByUsername(loginDto.Username);
            if (account == null || account.Status == 1) // status block
                return null;
            else
            {
                using var hmac = new HMACSHA512(account.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != account.PasswordHash[i])
                    {
                        return null;
                    }
                }


                return new UserDto
                {
                    Id = account.AccountID,
                    Email = account.Email,
                    Token = _tokenService.CreateToken(account),
                    RoleId = (int)account.RoleId,
                    AccountName = account.Name,
                    Username = account.Username,
                    isNewAccount = false
                };
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAllAsync();
        }
    }
}
