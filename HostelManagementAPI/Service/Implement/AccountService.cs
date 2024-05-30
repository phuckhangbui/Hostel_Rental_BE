using AutoMapper;
using DTOs;
using DTOs.Account;
using DTOs.AccountAuthentication;
using Google.Apis.Auth;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;
using Service.Mail;
using System.Security.Cryptography;
using System.Text;


namespace Service.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        public AccountService(IAccountRepository accountRepository, ITokenService tokenService, IMapper mapper, IMailService mailService)
        {
            _accountRepository = accountRepository; _tokenService = tokenService; _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<AccountDto> GetAccountLoginByUsername(LoginDto loginDto)
        {
            AccountDto accountDto = await _accountRepository.GetAccountLoginByUsername(loginDto.Username);
            if (accountDto == null || accountDto.Status == 1) // status block
                return null;
            else
            {
                using var hmac = new HMACSHA512(accountDto.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != accountDto.PasswordHash[i])
                    {
                        return null;
                    }
                }

                accountDto.IsNewAccount = false;
                accountDto.Token = _tokenService.CreateToken(accountDto);

                var refreshToken = _tokenService.GenerateRefreshToken();
                accountDto.RefreshToken = refreshToken;
                accountDto.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                await _accountRepository.UpdateAccount(accountDto);
                return accountDto;

            }
        }

        public async Task<IEnumerable<AccountViewDto>> GetAllAccounts()
        {
            return _accountRepository.GetAllAsync().Result.Select(x => new AccountViewDto
            {
                AccountId = x.AccountId,
                Email = x.Email,
                Name = x.Name,
                Status = x.Status
            }).ToList();
        }

        public async Task<AccountLoginDto> Login(EmailLoginDto login)
        {
            AccountDto accountDto = await _accountRepository.GetAccountByEmail(login.Email);

            if (accountDto != null)
            {
                if ((bool)accountDto.IsLoginWithGmail)
                {
                    throw new ServiceException("The account is created by login with Google, please use login with Google");
                }
                using var hmac = new HMACSHA512(accountDto.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != accountDto.PasswordHash[i])
                    {
                        return null;
                    }


                    accountDto.IsNewAccount = false;
                    accountDto.Token = _tokenService.CreateToken(accountDto);

                    var refreshToken = _tokenService.GenerateRefreshToken();
                    accountDto.RefreshToken = refreshToken;
                    accountDto.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                    await _accountRepository.UpdateAccount(accountDto);

                    return _mapper.Map<AccountLoginDto>(accountDto); ;
                }
            }
            throw new ServiceException("No account associate with this email");
        }


        public async Task RegisterEmail(EmailRegisterDto emailRegisterDto)
        {
            AccountDto accountDto = await _accountRepository.GetAccountByEmail(emailRegisterDto.Email);
            if (accountDto != null)
            {
                throw new ServiceException("This email has already been used. Please choose other email");
            }

            Random random = new Random();
            var tempPassword = random.Next(111111, 999999).ToString();


            using var hmac = new HMACSHA512();
            AccountDto newAccount = new AccountDto
            {
                Email = emailRegisterDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(tempPassword)),
                PasswordSalt = hmac.Key,
                CreatedDate = DateTime.Now,
                Status = 1, // k sure cho docs
                RoleId = 3, // k sure cho docs
                IsLoginWithGmail = false
            };

            await _accountRepository.CreateAccount(newAccount);

            //send mail here for the passwords
            _mailService.SendMail(SendAccountPassword.SendInitPassword(emailRegisterDto.Email, tempPassword));
        }

        public async Task ForgetPassword(EmailRegisterDto emailRegisterDto)
        {
            AccountDto accountDto = await _accountRepository.GetAccountByEmail(emailRegisterDto.Email);
            if (accountDto == null)
            {
                throw new ServiceException("No account associate with this email");
            }

            Random random = new Random();
            var tempPassword = random.Next(111111, 999999).ToString();

            using var hmac = new HMACSHA512();
            accountDto.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(tempPassword));
            accountDto.PasswordSalt = hmac.Key;

            await _accountRepository.UpdateAccount(accountDto);

            //send mail here for the passwords
            _mailService.SendMail(SendAccountPassword.SendInitPassword(emailRegisterDto.Email, tempPassword));
        }

        public async Task<AccountLoginDto> ConfirmPassword(ConfirmPasswordDtos confirmPasswordDtos)
        {
            AccountDto accountDto = await _accountRepository.GetAccountByEmail(confirmPasswordDtos.Email);
            if (accountDto != null)
            {
                if (accountDto.PasswordHash == null)
                {
                    throw new ServiceException("The account is created by login with Google, please use login with Google");
                }
                using var hmac = new HMACSHA512(accountDto.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(confirmPasswordDtos.SystemPassword));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != accountDto.PasswordHash[i])
                    {
                        return null;
                    }
                }

                accountDto.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(confirmPasswordDtos.NewPassword));
                accountDto.PasswordSalt = hmac.Key;
                accountDto.Status = 2; // fix later
                try
                {
                    await _accountRepository.UpdateAccount(accountDto);
                }
                catch (Exception ex)
                {
                    throw new ServiceException("Cannot update the password");
                }
                accountDto.Token = _tokenService.CreateToken(accountDto);

                return _mapper.Map<AccountLoginDto>(accountDto);
            }
            else throw new ServiceException("No account associate with this email");
        }

        public async Task<AccountLoginDto> LoginWithGoogle(LoginWithGoogleDto loginWithGoogle)
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(loginWithGoogle.IdTokenString);

            string userEmail = payload.Email;

            AccountDto accountDto = await _accountRepository.GetAccountByEmail(userEmail);
            if (accountDto != null)
            {
                if ((bool)!accountDto.IsLoginWithGmail)
                {
                    throw new ServiceException("This email has already been used and associated with an password, please choose other login method");
                }
                accountDto.Token = _tokenService.CreateToken(accountDto);
                var refreshToken = _tokenService.GenerateRefreshToken();
                accountDto.RefreshToken = refreshToken;
                accountDto.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                await _accountRepository.UpdateAccount(accountDto);

                return _mapper.Map<AccountLoginDto>(accountDto);
            }
            return null;
        }

        public async Task<AccountLoginDto> RegisterWithGoogle(LoginWithGoogleDto loginWithGoogle)
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(loginWithGoogle.IdTokenString);

            string userEmail = payload.Email;

            AccountDto accountDto = await _accountRepository.GetAccountByEmail(userEmail);
            if (accountDto != null)
            {
                throw new ServiceException("This email has already been used. Please choose other email");
            }

            AccountDto newAccount = new AccountDto
            {
                Email = payload.Email,
                Name = payload.Name,
                RoleId = 3, // fix later
                CreatedDate = DateTime.Now,
                IsLoginWithGmail = true
            };
            var refreshToken = _tokenService.GenerateRefreshToken();
            newAccount.RefreshToken = refreshToken;
            newAccount.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _accountRepository.CreateAccount(newAccount);

            accountDto.Token = _tokenService.CreateToken(accountDto);
            return _mapper.Map<AccountLoginDto>(accountDto);
        }

        public async Task Logout(int accountId)
        {
            var account = await _accountRepository.GetAccountById(accountId);

            account.RefreshToken = null;
            account.RefreshTokenExpiryTime = DateTime.MinValue;

            await _accountRepository.UpdateAccount(account);
        }


        public async Task ActiveAccount(int idAccount)
        {
            var account = _accountRepository.GetAccountById(idAccount);
            account.Result.Status = 0;
            await _accountRepository.UpdateAccount(account.Result);
        }

        public async Task UnactiveAccount(int idAccount)
        {
            var account = _accountRepository.GetAccountById(idAccount);
            account.Result.Status = 1;
            await _accountRepository.UpdateAccount(account.Result);
        }

        public async Task<AccountViewDetail> GetAccountById(int id)
        {
            var account = await _accountRepository.GetAccountById(id);
            AccountViewDetail accountViewDetail = _mapper.Map<AccountViewDetail>(account);
            return accountViewDetail;
        }

        public async Task<AccountLoginDto> RefreshToken(TokenApiDto tokenApiDto)
        {
            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var accountId = int.Parse(principal.Claims.First(i => i.Type == "AccountId").Value);

            var accountDto = await _accountRepository.GetAccountById(accountId);

            if (accountDto is null || accountDto.RefreshToken != refreshToken || accountDto.RefreshTokenExpiryTime <= DateTime.Now)
                return null;

            var newAccessToken = _tokenService.CreateToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            accountDto.Token = newAccessToken;
            accountDto.RefreshToken = newRefreshToken;

            await _accountRepository.UpdateAccount(accountDto);

            return _mapper.Map<AccountLoginDto>(accountDto);
        }

        public async Task<CustomerViewAccount> GetAccountProfileById(int id)
        {
            var account = await _accountRepository.GetAccountProfileById(id);
            CustomerViewAccount customerViewAccount = _mapper.Map<CustomerViewAccount>(account);
            return customerViewAccount;
        }
    }
}
