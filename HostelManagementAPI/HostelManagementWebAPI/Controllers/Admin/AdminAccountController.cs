using BusinessObject.Models;
using DTOs.Account;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Implement;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers.Admin
{
    public class AdminAccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public AdminAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize("Admin")]
        [HttpGet("/admin/accounts")]
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

        [Authorize("Admin")]
        [HttpPost("/admin/account/active")]
        public async Task<ActionResult> ActiveAccount(int idaccount)
        {
            try
            {
                await _accountService.ActiveAccount(idaccount);
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

        [Authorize("Admin")]
        [HttpPost("/admin/account/block")]
        public async Task<ActionResult> BlockAccount(int idaccount)
        {
            try
            {
                await _accountService.UnactiveAccount(idaccount);
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

        [Authorize("Admin")]
        [HttpGet("/admin/accounts/detail/{accountID}")]
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
    }
}
