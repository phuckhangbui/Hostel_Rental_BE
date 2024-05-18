using BusinessObject.Models;

namespace Service.Interface
{
    public interface ITokenService
    {
        string CreateToken(Account account);
    }
}
