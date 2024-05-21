using DTOs;
using HostelManagementWebAPI.MessageStatusResponse;
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
        public async Task<ActionResult<AccountDto>> Login(EmailLoginDto login)
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
            catch (Exception ex)
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
        public async Task<ActionResult<AccountDto>> ConfirmPassword(ConfirmPasswordDtos confirmPasswordDtos)
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
        public async Task<ActionResult<AccountDto>> LoginWithGoogle(LoginWithGoogleDto loginWithGoogle)
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
        public async Task<ActionResult<AccountDto>> RegisterWithGoogle(LoginWithGoogleDto loginWithGoogle)
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
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseStatus(400));
            }
        }
    }
}
