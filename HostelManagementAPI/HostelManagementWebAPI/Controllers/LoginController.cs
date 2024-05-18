using BusinessObject.Dtos;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAdminOrStaff(LoginDto loginDto)
        {
            var accountLogin = await _accountService.getAccountLogin(loginDto);
            if (accountLogin != null)
            {
                //HttpContext.Session.SetString("token", accountLogin.Token);
                return Ok(accountLogin);
            }
            else
            {
                return Unauthorized(new ApiResponseStatus(401));
            }
        }
    }
}
