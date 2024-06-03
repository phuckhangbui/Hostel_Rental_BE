using DTOs.Room;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
	public class RoomController : BaseApiController
	{
		private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

		[Authorize(Policy = "Owner")]
		[HttpPut("rooms/{roomId}")]
		public async Task<ActionResult> UpdateRoom(int roomId, [FromBody] UpdateRoomRequestDto updateRoomRequestDto)
		{
			try
			{
				await _roomService.UpdateRoom(roomId, updateRoomRequestDto);
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

		[Authorize(Policy = "Owner")]
		[HttpPut("rooms/{roomId}/status/{status}")]
		public async Task<ActionResult> ChangeRoomStatus(int roomId, int status)
		{
			try
			{
				await _roomService.ChangeRoomStatus(roomId, status);
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

		[HttpGet("rooms/{roomId}")]
		public async Task<ActionResult> GetRoomDetailById(int roomId)
		{
			try
			{
				var room = await _roomService.GetRoomDetailByRoomId(roomId);
				return Ok(room);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponseStatus(500, ex.Message));
			}
		}

		[HttpGet("rooms/{hostelId}/list")]
		public async Task<ActionResult> GetListRoomByHostelId(int hostelId)
		{
			try
			{
				var rooms = await _roomService.GetListRoomsByHostelId(hostelId);
				return Ok(rooms);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponseStatus(500, ex.Message));
			}
		}

		[Authorize(Policy = "Owner")]
		[HttpPost("rooms")]
		public async Task<ActionResult> Create([FromBody] CreateRoomRequestDto createRoomRequestDto)
		{
			try
			{
				var result = await _roomService.CreateRoom(createRoomRequestDto);
				return Ok(result);
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

		[Authorize(Policy = "Owner")]
		[HttpPost("rooms/{roomId}/images")]
		public async Task<ActionResult> UploadImage(int roomId, IFormFileCollection imageFiles)
		{
			try
			{
				if (imageFiles == null || imageFiles.Count == 0)
				{
					return BadRequest(new ApiResponseStatus(400, "Invalid image file."));
				}

				await _roomService.UploadRoomImage(imageFiles, roomId);
				return Ok(new ApiResponseStatus(200, "Image uploaded successfully."));
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

        [HttpGet("rooms/{hostelId}/roomImages")]
        public async Task<ActionResult> ViewAllRoomImageForHostel(int hostelId)
        {
            try
            {
                List<string> hostels = await _roomService.GetRoomImagesByHostelId(hostelId);
                return Ok(hostels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpPost("roomServiceAdd")]

        public async Task<IActionResult> AddRoomServices([FromBody] AddRoomServicesDto roomServicesDto)
        {
            try
            {
                await _roomService.AddRoomService(roomServicesDto);
                return Ok("Add Room Services Complete!");
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

        [HttpDelete("{roomId}/{serviceId}")]
        public async Task<IActionResult> RemoveRoomService(int roomId, int serviceId)
		{
			try
			{
				await _roomService.RemoveRoomServiceAsync(roomId, serviceId);
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
