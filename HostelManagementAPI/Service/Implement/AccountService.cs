using AutoMapper;
using BusinessObject.Models;
using DTOs;
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


                return new AccountDto
                {
                    AccountId = account.AccountID,
                    Email = account.Email,
                    Token = _tokenService.CreateToken(account),
                    RoleId = (int)account.RoleId,
                    Name = account.Name,
                    Username = account.Username,
                    IsNewAccount = false
                };
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<AccountDto> Login(EmailLoginDto login)
        {
            Account account = await _accountRepository.GetAccountByEmail(login.Email);
            if (account != null)
            {
                if ((bool)account.IsLoginWithGmail)
                {
                    throw new ServiceException("The account is created by login with Google, please use login with Google");
                }
                using var hmac = new HMACSHA512(account.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != account.PasswordHash[i])
                    {
                        return null;
                    }
                }

                AccountDto accountDto = _mapper.Map<AccountDto>(account);
                accountDto.IsNewAccount = false;
                accountDto.Token = _tokenService.CreateToken(account);
                return accountDto;
            }
            throw new ServiceException("No account associate with this email");
        }


        public async Task RegisterEmail(EmailRegisterDto emailRegisterDto)
        {
            Account account = await _accountRepository.GetAccountByEmail(emailRegisterDto.Email);
            if (account != null)
            {
                throw new ServiceException("This email has already been used. Please choose other email");
            }

            Random random = new Random();
            var tempPassword = random.Next(111111, 999999).ToString();


            using var hmac = new HMACSHA512();
            Account newAccount = new Account
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
            Account account = await _accountRepository.GetAccountByEmail(emailRegisterDto.Email);
            if (account == null)
            {
                throw new ServiceException("No account associate with this email");
            }

            Random random = new Random();
            var tempPassword = random.Next(111111, 999999).ToString();

            using var hmac = new HMACSHA512();
            account.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(tempPassword));
            account.PasswordSalt = hmac.Key;

            await _accountRepository.UpdateAsync(account);

            //send mail here for the passwords
            _mailService.SendMail(SendAccountPassword.SendInitPassword(emailRegisterDto.Email, tempPassword));
        }

        public async Task<AccountDto> ConfirmPassword(ConfirmPasswordDtos confirmPasswordDtos)
        {
            Account account = await _accountRepository.GetAccountByEmail(confirmPasswordDtos.Email);
            if (account != null)
            {
                if (account.PasswordHash == null)
                {
                    throw new ServiceException("The account is created by login with Google, please use login with Google");
                }
                using var hmac = new HMACSHA512(account.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(confirmPasswordDtos.SystemPassword));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != account.PasswordHash[i])
                    {
                        return null;
                    }
                }

                account.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(confirmPasswordDtos.NewPassword));
                account.PasswordSalt = hmac.Key;
                account.Status = 2; // fix later
                try
                {
                    await _accountRepository.UpdateAccount(account);
                }
                catch (Exception ex)
                {
                    throw new ServiceException("Cannot update the password");
                }
                AccountDto accountDto = _mapper.Map<AccountDto>(account);
                accountDto.Token = _tokenService.CreateToken(account);
                return accountDto;
            }
            else throw new ServiceException("No account associate with this email");
        }

        public async Task<AccountDto> LoginWithGoogle(LoginWithGoogleDto loginWithGoogle)
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(loginWithGoogle.IdTokenString);

            string userEmail = payload.Email;

            Account account = await _accountRepository.GetAccountByEmail(userEmail);
            if (account != null)
            {
                if ((bool)!account.IsLoginWithGmail)
                {
                    throw new ServiceException("This email has already been used and associated with an password, please choose other login method");
                }
                AccountDto accountDto = _mapper.Map<AccountDto>(account);
                accountDto.Token = _tokenService.CreateToken(account);
                return accountDto;
            }
            return null;
        }

        public async Task<AccountDto> RegisterWithGoogle(LoginWithGoogleDto loginWithGoogle)
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(loginWithGoogle.IdTokenString);

            string userEmail = payload.Email;

            Account account = await _accountRepository.GetAccountByEmail(userEmail);
            if (account != null)
            {
                throw new ServiceException("This email has already been used. Please choose other email");
            }

            Account newAccount = new Account
            {
                Email = payload.Email,
                Name = payload.Name,
                RoleId = 3, // fix later
                CreatedDate = DateTime.Now,
                IsLoginWithGmail = true
            };

            await _accountRepository.CreateAccount(newAccount);

            AccountDto accountDto = _mapper.Map<AccountDto>(account);
            accountDto.Token = _tokenService.CreateToken(account);
            return accountDto;
        }
    }
}
