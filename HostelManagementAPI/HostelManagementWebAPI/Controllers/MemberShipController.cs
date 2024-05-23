using DTOs;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class MemberShipController : BaseApiController
    {
        private readonly IMemberShipService _memberShipService;

        public MemberShipController(IMemberShipService memberShipService)
        {
            _memberShipService = memberShipService;
        }

        [HttpPost("admin-create-membership")]
        public async Task<ActionResult> CreateMembership([FromBody] CreateMemberShipDto createMembershipRequestDto)
        {
            try
            {
                if (createMembershipRequestDto == null)
                {
                       return BadRequest(new ApiResponseStatus(400, "Invalid request."));
                }
                if (createMembershipRequestDto.Month <= 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Month must be at least 1"));
                }
                if (createMembershipRequestDto.MemberShipFee <= 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Fee must be larger than 0"));
                }
                await _memberShipService.CreateMemberShip(createMembershipRequestDto);
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
    }
}
