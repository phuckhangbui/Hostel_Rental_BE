using BusinessObject.Dtos;
using BusinessObject.Models;
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

        public async Task<UserDto> getAccountLogin(LoginDto loginDto)
        {
            Account account = await _accountRepository.getAccountLoginByUsername(loginDto.Username);
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

                if (loginDto.FirebaseRegisterToken.IsNullOrEmpty())
                {

                }
                else if (!loginDto.FirebaseRegisterToken.Equals(account.FirebaseToken))
                {
                    var firebaseTokenExistedAccount = await _accountRepository.FirebaseTokenExisted(loginDto.FirebaseRegisterToken);
                    if (firebaseTokenExistedAccount != null)
                    {
                        firebaseTokenExistedAccount.FirebaseToken = null;
                        await _accountRepository.UpdateAsync(firebaseTokenExistedAccount);
                    }
                    account.FirebaseToken = loginDto.FirebaseRegisterToken;
                    await _accountRepository.UpdateAsync(account);
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


        public async Task<Account> FirebaseTokenExisted(string firebaseToken)
        {
            return await _accountRepository.FirebaseTokenExisted(firebaseToken);
        }

        public Task<Account> GetAccountAsync(string username)
        {
            return _accountRepository.getAccountLoginByUsername(username);
        }
    }
}
