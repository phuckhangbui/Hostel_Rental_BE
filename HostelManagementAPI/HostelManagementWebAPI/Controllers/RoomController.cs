﻿using DTOs.Room;
using DTOs.RoomAppointment;
using DTOs.RoomService;
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
		public async Task<ActionResult> UpdateRoom(int roomId, [FromBody] RoomRequestDto updateRoomRequestDto)
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
            catch (ServiceException ex)
            {
                return BadRequest(new ApiResponseStatus(400, ex.Message));
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
            catch (ServiceException ex)
            {
                return BadRequest(new ApiResponseStatus(400, ex.Message));
            }
            catch (Exception ex)
			{
				return StatusCode(500, new ApiResponseStatus(500, ex.Message));
			}
		}

        [HttpGet("rooms/member/{hostelId}/list")]
        public async Task<ActionResult> GetListRoomByHostelIdForMember(int hostelId)
        {
            try
            {
                var rooms = await _roomService.GetListRoomByHostelIdForMember(hostelId);
                return Ok(rooms);
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
		[HttpPost("rooms")]
		public async Task<ActionResult> Create([FromBody] CreateRoomRequestDto createRoomRequestDto)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        [HttpGet("rooms/appointment")]
        public async Task<ActionResult> GetRoomAppointmentList()
        {
            try
            {
                var roomAppointments = await _roomService.GetRoomAppointmentsAsync();
                return Ok(roomAppointments);
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

        [HttpPost("rooms/appointment")]
        public async Task<ActionResult> CreateRoomAppointment([FromBody] CreateRoomAppointmentDto createRoomAppointmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var roomStatus = await _roomService.GetRoomDetailByRoomId(createRoomAppointmentDto.RoomId);
                if(roomStatus.Status != 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Room is not available"));
                }
                var isUpdatedStatus = await _roomService.UpdateRoomStatus(createRoomAppointmentDto.RoomId, 1);
                if(isUpdatedStatus)
                {
                    await _roomService.CreateRoomAppointmentAsync(createRoomAppointmentDto);
                    return Ok(new ApiResponseStatus(200, "Create appoiment success"));
                } 
                return BadRequest(new ApiResponseStatus(400, "Fail to create appoitment"));
                
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

        [HttpGet("rooms/appointment/details/{appointmentId}")]
        public async Task<ActionResult> GetApppointmentDetails(int appointmentId)
        {
            try
            {
                var appointment = await _roomService.GetAppointmentById(appointmentId);
                if (appointment == null)
                {
                    return BadRequest(new ApiResponseStatus(400, "Appointment not found"));
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpGet("owner/appointment/details/{roomID}")]
        public async Task<ActionResult> GetApppointmentToCreateContract(int roomID)
        {
            try
            {
                var appointment = await _roomService.GetApppointmentToCreateContract(roomID);
                if (appointment == null)
                {
                    return BadRequest(new ApiResponseStatus(400, "Appointment not found"));
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }


        [HttpPut("rooms/service-update-select-status/{roomId}")]
        public async Task<IActionResult> UpdateRoomServices(int roomId, [FromBody] List<RoomServiceUpdateDto> updates)
        {
            if (updates == null || updates.Count == 0)
            {
                return BadRequest("No updates provided.");
            }

            try
            {
                await _roomService.UpdateRoomServicesIsSelectStatusAsync(roomId, updates);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(policy: "Owner")]
        [HttpGet("owner/rooms/appointment/{hostelID}")]
        public async Task<ActionResult> GetRoomAppointmentListByOwner(int hostelID)
        {
            try
            {
                var roomAppointments = await _roomService.GetRoomAppointmentListByOwner(hostelID);
                return Ok(roomAppointments);
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

        [Authorize(policy: "Owner")]
        [HttpPut("owner/rooms/appointment/cancel/{appointmentID}")]
        public async Task<ActionResult> CancelAppointmentByOwner(int appointmentID)
        {
            try
            {
                await _roomService.CancelAppointmentRoom(appointmentID);
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

        //      [HttpPost("roomServiceAdd")]
        //      public async Task<IActionResult> AddRoomServices([FromBody] AddRoomServicesDto roomServicesDto)
        //      {
        //          try
        //          {
        //              await _roomService.AddRoomService(roomServicesDto);
        //              return Ok("Add Room Services Complete!");
        //          }
        //          catch (ServiceException ex)
        //          {
        //              return BadRequest(new ApiResponseStatus(400, ex.Message));
        //          }
        //          catch (Exception ex)
        //          {
        //              return StatusCode(500, new ApiResponseStatus(500, ex.Message));
        //          }
        //      }

        //      [HttpDelete("{roomId}/{serviceId}")]
        //      public async Task<IActionResult> RemoveRoomService(int roomId, int serviceId)
        //{
        //	try
        //	{
        //		await _roomService.RemoveRoomServiceAsync(roomId, serviceId);
        //		return Ok();
        //	}
        //          catch (ServiceException ex)
        //          {
        //              return BadRequest(new ApiResponseStatus(400, ex.Message));
        //          }
        //          catch (Exception ex)
        //          {
        //              return StatusCode(500, new ApiResponseStatus(500, ex.Message));
        //          }


        //      }

        //      [HttpGet("rooms/{roomId}/roomServices")]
        //      public async Task<ActionResult> GetRoomServicesByRoomId(int roomId)
        //      {
        //          try
        //          {
        //               var roomServices = await _roomService.GetRoomServicesByRoomIdAsync(roomId);
        //              return Ok(roomServices);
        //          }
        //          catch (Exception ex)
        //          {
        //              return StatusCode(500, new ApiResponseStatus(500, ex.Message));
        //          }
        //      }
    }
}
