using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers.Admin
{
    [ApiController]
    public class AdminHotelsController : BaseApiController
    {
        private readonly IHostelService _hostelService;
        private readonly IRoomService _roomService;

        public AdminHotelsController(IHostelService hostelService, IRoomService roomService)
        {
            _hostelService = hostelService;
            _roomService = roomService;
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/hostels")]
        public async Task<ActionResult> GetHostels()
        {
            try
            {
                var hostels = await _hostelService.GetHostelsAdminView();
                return Ok(hostels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/hostels/detail/{hostelID}")]
        public async Task<ActionResult> GetHostelDetailAdminView(int hostelID)
        {
            try
            {
                var hostel = await _hostelService.GetHostelDetailAdminView(hostelID);
                return Ok(hostel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [Authorize(policy: "Admin")]
        [HttpGet("admin/hostels/rooms/detail/{hostelID}")]
        public async Task<ActionResult> GetHostelDetailWithRoomAdminView(int hostelID)
        {
            try
            {
                var hostel = await _roomService.GetHostelDetailWithRoomAdminView(hostelID);
                return Ok(hostel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }
    }
}
