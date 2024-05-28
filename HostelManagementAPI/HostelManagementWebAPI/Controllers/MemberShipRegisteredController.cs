using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Implement;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class MemberShipRegisteredController : BaseApiController
    {
        private readonly IMembershipRegisterService memberShipRegisteredService;
        public MemberShipRegisteredController(IMembershipRegisterService memberShipRegisteredService)
        {
            this.memberShipRegisteredService = memberShipRegisteredService;
        }

        [HttpGet("admin-get-all-membership")]
        public async Task<ActionResult> GetAllMembership()
        {
            try
            {
                var membershipRegistered = await memberShipRegisteredService.GetAllMemberships();
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
