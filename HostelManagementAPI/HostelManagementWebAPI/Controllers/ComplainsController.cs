using DTOs.Complain;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    public class ComplainsController : ODataController
    {
        private readonly IComplainService _complainService;

        public ComplainsController(IComplainService complainService)
        {
            _complainService = complainService;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ComplainDto>>> Get()
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

        [Authorize(Policy = "Member")]
        [HttpPost("complains")]
        public async Task<ActionResult> Post([FromBody] CreateComplainDto createComplainRequestDto)
        {
            try
            {
                await _complainService.CreateComplain(createComplainRequestDto);
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

        [Authorize(Policy = "OwnerAndAdmin")]
        [HttpPut("complains")]
        public async Task<ActionResult> Put([FromODataUri] int key, [FromBody] UpdateComplainStatusDto updateComplainRequestDto)
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

        //[HttpGet("complains/{complainId}")]
        //public async Task<ActionResult<ComplainDto>> GetComplain(int complainId)
        //{
        //    try
        //    {
        //        return await _complainService.GetComplainById(complainId);
        //    }
        //    catch (ServiceException ex)
        //    {
        //        return BadRequest(new ApiResponseStatus(400, ex.Message));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ApiResponseStatus(500, ex.Message));
        //    }
        //}

        //[HttpGet("complains/account/{accountId}")]
        //public async Task<ActionResult<IEnumerable<ComplainDto>>> GetComplainsByAccountCreated(int accountId)
        //{
        //    try
        //    {
        //        var list = await _complainService.GetComplainsByAccountCreator(accountId);
        //        if (list == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(list);
        //    }
        //    catch (ServiceException ex)
        //    {
        //        return BadRequest(new ApiResponseStatus(400, ex.Message));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ApiResponseStatus(500, ex.Message));
        //    }
        //}

        //[HttpGet("complains/room/{roomId}")]
        //public async Task<ActionResult<IEnumerable<ComplainDto>>> GetComplainsByRoom(int roomId)
        //{
        //    try
        //    {
        //        var list = await _complainService.GetComplainsByRoom(roomId);
        //        if (list == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(list);
        //    }
        //    catch (ServiceException ex)
        //    {
        //        return BadRequest(new ApiResponseStatus(400, ex.Message));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new ApiResponseStatus(500, ex.Message));
        //    }
        //}
    }
}
