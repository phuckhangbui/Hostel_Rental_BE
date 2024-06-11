using DTOs.Membership;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class MemberShipPackageController : BaseApiController
    {

        private readonly IMemberShipService _memberShipService;

        public MemberShipPackageController(IMemberShipService memberShipService)
        {
            _memberShipService = memberShipService;
        }

        [HttpGet("get-memberships-active")]
        public async Task<ActionResult> GetMemberships()
        {
            try
            {
                var memberships = await _memberShipService.GetMembershipsActive();
                return Ok(memberships);
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
