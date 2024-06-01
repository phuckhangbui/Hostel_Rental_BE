using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class MemberShipRegisteredController : BaseApiController
    {
        private readonly IMembershipRegisterService memberShipRegisteredService;
        private readonly IAccountService accountService;
        public MemberShipRegisteredController(IMembershipRegisterService memberShipRegisteredService, IAccountService accountService)
        {
            this.memberShipRegisteredService = memberShipRegisteredService;
            this.accountService = accountService;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("admin/memberships")]
        public async Task<ActionResult> GetAllMembership()
        {
            try
            {
                var membershipRegistered = await accountService.GetAllMemberShip();
                return Ok(membershipRegistered);
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

        [Authorize(Policy = "Admin")]
        [HttpGet("admin/memberships/detail/{accountId}")]
        public async Task<ActionResult> GetDetailMemberShipRegister(int accountId)
        {
            try
            {
                var membershipRegistered = await memberShipRegisteredService.GetAllMembershipPackageInAccount(accountId);
                return Ok(membershipRegistered);
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

        [Authorize(Policy = "Admin")]
        [HttpGet("admin/memberships/detail/information/{accountId}")]
        public async Task<ActionResult> GetDetailMemberShipRegisterInformation(int accountId)
        {
            try
            {
                var membershipRegistered = await accountService.GetDetailMemberShipRegisterInformation(accountId);
                return Ok(membershipRegistered);
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
    }
}
