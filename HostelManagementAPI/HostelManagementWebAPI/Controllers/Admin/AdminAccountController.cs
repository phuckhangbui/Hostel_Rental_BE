using DTOs.Account;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers.Admin
{
    [ApiController]
    public class AdminAccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public AdminAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/accounts")]
        public async Task<ActionResult<IEnumerable<AccountViewDto>>> GetAllAccountsInFlatform()
        {
            var accounts = await _accountService.GetAllAccounts();
            if (accounts != null)
            {
                return Ok(accounts);
            }
            else
            {
                return null;
            }
        }

        [Authorize(policy : "Admin")]
        [HttpPut("admin/account/active/{idaccount}")]
        public async Task<ActionResult> ActiveAccount(AccountStatusDto idaccount)
        {
            try
            {
                await _accountService.ActiveAccount(idaccount.AccountId);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return BadRequest(new ApiResponseStatus(400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [Authorize(policy: "Admin")]
        [HttpPut("admin/account/block/{idaccount}")]
        public async Task<ActionResult> BlockAccount(AccountStatusDto idaccount)
        {
            try
            {
                await _accountService.UnactiveAccount(idaccount.AccountId);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return BadRequest(new ApiResponseStatus(400, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/accounts/detail/{accountID}")]
        public async Task<ActionResult<AccountViewDetail>> GetAccountDetailById(int accountID)
        {
            try
            {
                var account = await _accountService.GetAccountById(accountID);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [Authorize(policy: "Admin")]
        [HttpDelete("admin/accounts/detail/delete/{accountID}")]
        public async Task<ActionResult> DeleteAcount(int accountID)
        {
            try
            {
                await _accountService.DeleteAcount(accountID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, new ApiResponseStatus(404, ex.Message));
            }
        }
    }
}
