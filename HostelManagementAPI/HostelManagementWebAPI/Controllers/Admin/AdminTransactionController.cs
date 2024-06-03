using DTOs.Account;
using DTOs.MemberShipRegisterTransaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers.Admin
{
    [ApiController]
    public class AdminTransactionController : BaseApiController
    {
        private readonly IMembershipRegisterService _membershipRegisterService;
        public AdminTransactionController(IMembershipRegisterService membershipRegisterService)
        {
            _membershipRegisterService = membershipRegisterService;
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/transactions")]
        public async Task<ActionResult<IEnumerable<ViewTransactionMembership>>> GetAllTransactionInAdmin()
        {
            var accounts = await _membershipRegisterService.GetAllTransactionInAdmin();
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
