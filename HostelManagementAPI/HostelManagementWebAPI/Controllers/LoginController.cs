using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    public class LoginController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }


    }
}
