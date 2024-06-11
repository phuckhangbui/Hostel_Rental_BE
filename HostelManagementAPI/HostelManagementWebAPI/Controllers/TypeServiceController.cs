using DTOs.TypeService;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class TypeServiceController : BaseApiController
    {
        private readonly ITypeServiceService _typeServiceService;

        public TypeServiceController(ITypeServiceService typeServiceService)
        {
            _typeServiceService = typeServiceService;
        }

        //[Authorize(Policy = "Admin")]
        //[HttpPost("admin-create-type-service")]
        //public async Task<ActionResult> CreateTypeService([FromBody] CreateTypeServiceDto typeService)
        //{
        //    try
        //    {
        //        var checkTypeServiceNameExist = await _typeServiceService.CheckTypeServiceNameExist(typeService.TypeName);
        //        if (typeService == null)
        //        {
        //            return BadRequest(new ApiResponseStatus(400, "Invalid request."));
        //        }
        //        if(string.IsNullOrEmpty(typeService.TypeName))
        //        {
        //            return BadRequest(new ApiResponseStatus(400, "Please input type service name"));
        //        }
        //        if(checkTypeServiceNameExist)
        //        {
        //            return BadRequest(new ApiResponseStatus(400, "Type name already exist"));
        //        }
        //        await _typeServiceService.CreateTypeService(typeService);
        //        return Ok();
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

        ////[Authorize(Policy = "Admin")]
        //[HttpGet("admin-get-all-type-service")]
        //public async Task<ActionResult> GetAllTypeService()
        //{
        //    try
        //    {
        //        var typeService = await _typeServiceService.GetAllTypeService();
        //        return Ok(typeService);
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

        //[Authorize(Policy = "Admin")]
        //[HttpPut("admin-update-type-service-name")]
        //public async Task<ActionResult> UpdateTypeServiceName([FromBody] UpdateTypeServiceDto typeService)
        //{
        //    try
        //    {
        //        var checkTypeService = await _typeServiceService.CheckExistTypeService(typeService.TypeServiceID);
        //        if (!checkTypeService)
        //        {
        //            return BadRequest(new ApiResponseStatus(400, "Can't found type service"));
        //        }
        //        if (string.IsNullOrEmpty(typeService.TypeName))
        //        {
        //            return BadRequest(new ApiResponseStatus(400, "Please input type service name"));
        //        }
        //        var checkTypeServiceNameExist = await _typeServiceService.CheckTypeServiceNameExist(typeService.TypeName);
        //        if (checkTypeServiceNameExist)
        //        {
        //            return BadRequest(new ApiResponseStatus(400, "Type name already exist"));
        //        }

        //        var result = await _typeServiceService.UpdateTypeServiceName(typeService);
        //        if (result)
        //        {
        //            return Ok();
        //        }

        //        return BadRequest(new ApiResponseStatus(400, "Update type service failed"));
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

        [HttpGet("type-services")]
        public async Task<ActionResult> GetTypeServices()
        {
            try
            {
                var typeService = await _typeServiceService.GetAllTypeService();
                return Ok(typeService);
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