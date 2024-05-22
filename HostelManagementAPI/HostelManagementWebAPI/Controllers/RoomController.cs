using DTOs.Room;
using HostelManagementWebAPI.MessageStatusResponse;
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

		[HttpPut("{roomId}/status")]
		public async Task<ActionResult> ChangeRoomStatus(int roomId, [FromBody] int status)
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

		[HttpGet("{roomId}")]
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

		[HttpGet("{hostelId}/rooms")]
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

		[HttpPost]
		public async Task<ActionResult> Create([FromBody] CreateRoomRequestDto createRoomRequestDto)
		{
			try
			{
				await _roomService.CreateRoom(createRoomRequestDto);
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

		[HttpPost("{roomId}/images")]
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
	}
}
