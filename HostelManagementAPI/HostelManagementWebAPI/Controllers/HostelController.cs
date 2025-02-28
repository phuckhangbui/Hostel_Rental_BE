﻿using DTOs.Enum;
using DTOs.Hostel;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("hostels/types")]
        public ActionResult<IEnumerable<HostelTypeResponseDto>> GetHostelTypes()
        {
            try
            {
                var hostelTypes = new List<HostelTypeResponseDto>();
                foreach (HostelTypeEnum hostelType in Enum.GetValues(typeof(HostelTypeEnum)))
                {
                    var friendlyString = hostelType.ToFriendlyString();
                    hostelTypes.Add(new HostelTypeResponseDto { Key = hostelType.ToString(), Value = friendlyString });
                }

                return Ok(hostelTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [Authorize(Policy = "Owner")]
		[HttpPost("hostels")]
        public async Task<ActionResult> Create([FromBody] CreateHostelRequestDto createHostelRequestDto)
        {
            try
            {
                var result = await _hostelService.CreateHostel(createHostelRequestDto);
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

		[Authorize(Policy = "Owner")]
		[HttpGet("owner/{ownerId}/hostels")]
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

        [Authorize(Policy = "Owner")]
        [HttpGet("owner/hostels/{hostelId}")]
        public async Task<ActionResult> GetHostelDetailForOwner(int hostelId)
        {
            try
            {
                var hostels = await _hostelService.GetHostelDetailForOwner(hostelId);
                return Ok(hostels);
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

        [HttpGet("hostels/{hostelId}")]
		public async Task<ActionResult> GetHostelDetail(int hostelId)
		{
			try
			{
				var hostels = await _hostelService.GetHostelDetail(hostelId);
				return Ok(hostels);
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
		[HttpPut("hostels/{hostelId}")]
        public async Task<ActionResult> Update(int hostelId, [FromBody] UpdateHostelRequestDto updateHostelRequestDto)
        {
            try
            {
                await _hostelService.UpdateHostel(hostelId, updateHostelRequestDto);
                return Ok(new ApiResponseStatus(Ok().StatusCode, "Update hostel information successfully"));
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
		[HttpPut("hostels/{hostelId}/status/{status}")]
        public async Task<ActionResult> ChangeStatus(int hostelId, int status)
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

        [Authorize(Policy = "Owner")]
        [HttpPost("hostels/{hostelId}/images")]
        public async Task<ActionResult> UploadImage(int hostelId, IFormFileCollection imageFiles)
        {
            try
            {
                if (imageFiles == null || imageFiles.Count == 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Invalid image file."));
                }

                await _hostelService.UploadHostelImages(hostelId, imageFiles);
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
