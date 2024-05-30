using DTOs.Membership;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers.Admin
{
    [ApiController]
    public class AdminMemberShipController : BaseApiController
    {
        private readonly IMemberShipService _memberShipService;

        public AdminMemberShipController(IMemberShipService memberShipService)
        {
            _memberShipService = memberShipService;
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("admin-create-membership")]
        public async Task<ActionResult> CreateMembership([FromBody] CreateMemberShipDto createMembershipRequestDto)
        {
            try
            {
                var checkNameExist = await _memberShipService.CheckMembershipNameExist(createMembershipRequestDto.MemberShipName);
                if(checkNameExist)
                {
                    return BadRequest(new ApiResponseStatus(400, "Membership name already exists"));
                }
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

        [Authorize(Policy = "Admin")]
        [HttpPost("admin/packages/detail/update")]
        public async Task<ActionResult> UpdateMemberShip([FromBody] UpdateMemberShipAdminDto updateMemberShipAdminDto)
        {
            try
            {
                var checkNameExist = await _memberShipService.CheckMembershipNameExist(updateMemberShipAdminDto.MemberShipName);
                if (checkNameExist)
                {
                    return BadRequest(new ApiResponseStatus(400, "Membership name already exists"));
                }
                if (updateMemberShipAdminDto == null)
                {
                    return BadRequest(new ApiResponseStatus(400, "Invalid request."));
                }
                if (updateMemberShipAdminDto.Month <= 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Month must be at least 1"));
                }
                if (updateMemberShipAdminDto.MemberShipFee <= 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Fee must be larger than 0"));
                }
                await _memberShipService.UpdateMemberShip(updateMemberShipAdminDto);
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

        [Authorize(Policy = "Admin")]
        [HttpGet("admin-get-memberships-expire")]
        public async Task<ActionResult> GetMembershipsExpire()
        {
            try
            {
                var memberships = await _memberShipService.GetMembershipsExpire();
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

        [Authorize(Policy = "Admin")]
        [HttpGet("admin/packages")]
        public async Task<ActionResult> GetAllMemberShip()
        {
            try
            {
                var memberships = await _memberShipService.GetAllMemberships();
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

        [Authorize(Policy = "Admin")]
        [HttpGet("admin/packages/detail/{packageID}")]
        public async Task<ActionResult> GetDetailMemberShip(int packageID)
        {
            try
            {
                var membership = await _memberShipService.GetDetailMemberShip(packageID);
                return Ok(membership);
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
        [HttpPut("admin-deactivate-membership")]
        public async Task<ActionResult> DeactivateMembership([FromBody] UpdateMembershipDto updateMembershipDto)
        {
            try
            {
                if (updateMembershipDto == null)
                {
                    return BadRequest(new ApiResponseStatus(400, "Invalid request."));
                }
                if (updateMembershipDto.MemberShipID <= 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Invalid MemberShipID"));
                }
                var result = await _memberShipService.DeactivateMembership(updateMembershipDto);
                if (result)
                {
                    return Ok();
                }
                return BadRequest(new ApiResponseStatus(400, "Deactivate failed"));
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
        [HttpPut("admin-activate-membership")]
        public async Task<ActionResult> ActivateMembership([FromBody] UpdateMembershipDto updateMembershipDto)
        {
            try
            {
                if (updateMembershipDto == null)
                {
                    return BadRequest(new ApiResponseStatus(400, "Invalid request."));
                }
                if (updateMembershipDto.MemberShipID <= 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Invalid MemberShipID"));
                }
                var result = await _memberShipService.ActivateMembership(updateMembershipDto);
                if (result)
                {
                    return Ok();
                }
                return BadRequest(new ApiResponseStatus(400, "Deactivate failed"));
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
