using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;

        public TestController(ILogger<TestController> logger, IAccountService accountService, IAccountRepository accountRepository)
        {
            _logger = logger;
            _accountService = accountService;
            _accountRepository = accountRepository;
        }

        [HttpGet("/name")]
        public async Task<ActionResult<Account>> GetAsync(string username)
        {
            var account = await _accountRepository.GetAccountLoginByUsername(username);
            return Ok(account);
        }
    }
}
