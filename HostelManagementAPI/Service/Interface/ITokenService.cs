using DTOs.Account;

namespace Service.Interface
{
    public interface ITokenService
    {
        string CreateToken(AccountDto accountDto);
    }
}
