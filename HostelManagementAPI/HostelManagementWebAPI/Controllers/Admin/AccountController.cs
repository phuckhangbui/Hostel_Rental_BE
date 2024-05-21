﻿using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers.Admin
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("api/admin/accounts")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsInFlatform()
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
    }
}
