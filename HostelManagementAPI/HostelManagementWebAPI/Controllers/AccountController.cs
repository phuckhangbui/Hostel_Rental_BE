using DTOs;
using DTOs.Account;
using DTOs.AccountAuthentication;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccountLoginDto>> Login(EmailLoginDto login)
        {

            try
            {
                var user = await _accountService.Login(login);

                if (user == null)
                {
                    return Unauthorized(new ApiResponseStatus(401, "Wrong password"));
                }
                return Ok(user);
            }
            catch (ServiceException ex)
            {
                return Unauthorized(new ApiResponseStatus(401, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseStatus(400));
            }
        }

        [HttpPost("register/email")]
        public async Task<ActionResult> Register(EmailRegisterDto emailRegisterDto)
        {
            try
            {
                await _accountService.RegisterEmail(emailRegisterDto);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return BadRequest(new ApiResponseStatus(400, ex.Message));
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponseStatus(400));
            }
        }

        [HttpPost("forget/password")]
        public async Task<ActionResult> ForgetPassword(EmailRegisterDto emailRegisterDto)
        {
            try
            {
                await _accountService.ForgetPassword(emailRegisterDto);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return BadRequest(new ApiResponseStatus(400, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseStatus(400));
            }
        }

        [HttpPost("confirm/password")]
        public async Task<ActionResult<AccountLoginDto>> ConfirmPassword(ConfirmPasswordDtos confirmPasswordDtos)
        {
            try
            {
                var user = await _accountService.ConfirmPassword(confirmPasswordDtos);
                return Ok(user);
            }
            catch (ServiceException ex)
            {
                return BadRequest(new ApiResponseStatus(400, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseStatus(400));
            }
        }

        [HttpPost("login/google")]
        public async Task<ActionResult<AccountLoginDto>> LoginWithGoogle(LoginWithGoogleDto loginWithGoogle)
        {
            try
            {
                var user = await _accountService.LoginWithGoogle(loginWithGoogle);
                if (user == null)
                {
                    return Unauthorized(new ApiResponseStatus(401, "No account associate with this email"));
                }
                return Ok(user);
            }
            catch (ServiceException ex)
            {
                return Unauthorized(new ApiResponseStatus(401, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseStatus(400));
            }
        }

        [HttpPost("register/google")]
        public async Task<ActionResult<AccountLoginDto>> RegisterWithGoogle(LoginWithGoogleDto loginWithGoogle)
        {
            try
            {
                var user = await _accountService.RegisterWithGoogle(loginWithGoogle);
                return Ok(user);
            }
            catch (ServiceException ex)
            {
                return BadRequest(new ApiResponseStatus(400, ex.Message));
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponseStatus(400));
            }
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<AccountLoginDto>> Refresh(TokenApiDto tokenApiDto)
        {
            if (tokenApiDto is null)
                return BadRequest(new ApiResponseStatus(400, "Invalid client request"));

            try
            {
                var accountDto = await _accountService.RefreshToken(tokenApiDto);
                if (accountDto == null)
                {
                    return BadRequest(new ApiResponseStatus(400, "Invalid token"));
                }
                return Ok(accountDto);
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponseStatus(400));
            }


        }

        [HttpPost, Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                int accountId = GetLoginAccountId();
                await _accountService.Logout(accountId);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponseStatus(400));
            }

        }

        [HttpGet("profile")]
        public async Task<ActionResult<CustomerViewAccount>> GetAccountDetailById(int accountID)
        {
            try
            {
                var account = await _accountService.GetAccountProfileById(accountID);
                if (account == null)
                {
                    return NotFound(new ApiResponseStatus(404, "Account not found"));
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

    }
}
