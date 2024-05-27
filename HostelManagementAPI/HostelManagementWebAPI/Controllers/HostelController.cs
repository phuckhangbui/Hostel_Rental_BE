using DTOs.Hostel;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class HostelController : BaseApiController
    {
        private readonly IHostelService _hostelService;

        public HostelController(IHostelService hostelService)
        {
            _hostelService = hostelService;
        }

        [HttpPost("hostels")]
        public async Task<ActionResult> Create([FromBody] CreateHostelRequestDto createHostelRequestDto)
        {
            try
            {
                await _hostelService.CreateHostel(createHostelRequestDto);
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

        [HttpGet("hostels")]
        public async Task<ActionResult> GetHostels()
        {
            try
            {
                var hostels = await _hostelService.GetHostels();
                return Ok(hostels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpGet("hostels/{ownerId}")]
        public async Task<ActionResult> GetHostelsByOwner(int ownerId)
        {
            try
            {
                var hostels = await _hostelService.GetHostelsByOwner(ownerId);
                return Ok(hostels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpPut("hostels")]
        public async Task<ActionResult> Update([FromBody] UpdateHostelRequestDto updateHostelRequestDto)
        {
            try
            {
                await _hostelService.UpdateHostel(updateHostelRequestDto);
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

        [HttpPut("hostels/{hostelId}/status")]
        public async Task<ActionResult> ChangeStatus(int hostelId, [FromBody] int status)
        {
            try
            {
                await _hostelService.ChangeHostelStatus(hostelId, status);
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
