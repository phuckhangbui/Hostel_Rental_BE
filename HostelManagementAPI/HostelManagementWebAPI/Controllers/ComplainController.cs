using DTOs.Complain;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    public class ComplainController : BaseApiController
    {
        private readonly IComplainService _complainService;

        public ComplainController(IComplainService complainService)
        {
            _complainService = complainService;
        }

        [HttpGet("complains")]
        public async Task<ActionResult> GetComplains()
        {
            try
            {
                var complains = await _complainService.GetComplains();
                return Ok(complains);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpPost("complains")]
        public async Task<ActionResult> Create([FromBody] CreateComplainDto createComplainRequestDto)
        {
            int accountId = GetLoginAccountId();
            if (accountId == 0)
            {
                return Unauthorized(new ApiResponseStatus(401, "Unauthorized"));
            }

            try
            {
                await _complainService.CreateComplain(createComplainRequestDto, accountId);
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

        [HttpPut("complains")]
        public async Task<ActionResult> Update([FromBody] UpdateComplainStatusDto updateComplainRequestDto)
        {
            try
            {
                await _complainService.UpdateComplainStatus(updateComplainRequestDto);
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

        [HttpGet("complains/{complainId}")]
        public async Task<ActionResult<DisplayComplainDto>> GetComplain(int complainId)
        {
            try
            {
                return await _complainService.GetComplainById(complainId);
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

        [HttpGet("complains/account/{accountId}")]
        public async Task<ActionResult<IEnumerable<DisplayComplainDto>>> GetComplainsByAccountCreated(int accountId)
        {
            try
            {
                var list = await _complainService.GetComplainsByAccountCreator(accountId);
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
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

        [HttpGet("complains/room/{roomId}")]
        public async Task<ActionResult<IEnumerable<DisplayComplainDto>>> GetComplainsByRoom(int roomId)
        {
            try
            {
                var list = await _complainService.GetComplainsByRoom(roomId);
                if (list == null)
                {
                    return NotFound();
                }
                return Ok(list);
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
