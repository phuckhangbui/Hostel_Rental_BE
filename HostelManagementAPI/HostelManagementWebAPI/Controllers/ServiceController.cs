using DTOs.Service;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class ServiceController : BaseApiController
    {
        private readonly IRoomService _serviceService;
        private readonly ITypeServiceService _typeServiceService;
        public ServiceController(IRoomService serviceService, ITypeServiceService typeServiceService)
        {
            _serviceService = serviceService;
            _typeServiceService = typeServiceService;
        }


        [Authorize(policy: "Owner")]
        [HttpGet("owner/get-service/{roomID}")]
        public async Task<IActionResult> GetRoomServiceByRoom(int roomID)
        {
            var service = await _serviceService.GetRoomServicesByRoom(roomID);
            return Ok(service);
        }

        //        [HttpPost("owner-create-service")]
        //        public async Task<ActionResult> CreateService([FromBody] CreateServiceDto createService)
        //        {
        //            try
        //            {
        //                if (createService == null)
        //                {
        //                       return BadRequest(new ApiResponseStatus(400, "Invalid request."));
        //                }
        //                if (createService.ServicePrice <= 0)
        //                {
        //                    return BadRequest(new ApiResponseStatus(400, "Service price must be greater than 0."));
        //                }
        //                if(createService.TypeServiceID == null)
        //                {
        //                    return BadRequest(new ApiResponseStatus(400, "Type service ID is required."));
        //                }
        //                if (string.IsNullOrEmpty(createService.ServiceName))
        //                {
        //                    return BadRequest(new ApiResponseStatus(400, "Service name is required."));
        //                }
        //                var result = await _serviceService.CreateService(createService);
        //                if (result)
        //                {
        //                    return Ok();
        //                }
        //                return BadRequest(new ApiResponseStatus(400, "Failed to create new service"));
        //            }
        //            catch (ServiceException ex)
        //            {
        //                return BadRequest(new ApiResponseStatus(400, ex.Message));
        //            }
        //            catch (Exception ex)
        //            {
        //                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
        //            }
        //        }

        //        [HttpPut("owner-update-service")]
        //        public async Task<ActionResult> UpdateService([FromBody] UpdateServiceDto updateService)
        //        {
        //            try
        //            {                
        //                if (updateService == null)
        //                {
        //                    return BadRequest(new ApiResponseStatus(400, "Invalid request."));
        //                }
        //                var checkServiceExist = await _serviceService.CheckServiceExist(updateService.ServiceID);
        //                if (!checkServiceExist)
        //                {
        //                    return BadRequest(new ApiResponseStatus(400, "Service does not exist."));
        //                }
        //                var checkTypeServiceExist = await _typeServiceService.CheckExistTypeService(updateService.TypeServiceID);
        //                if (!checkTypeServiceExist)
        //                {
        //                    return BadRequest(new ApiResponseStatus(400, "Type service does not exist."));
        //                }
        //                if (updateService.ServicePrice <= 0)
        //                {
        //                    return BadRequest(new ApiResponseStatus(400, "Service price must be greater than 0."));
        //                }
        //                if (updateService.TypeServiceID == 0)
        //                {
        //                    return BadRequest(new ApiResponseStatus(400, "Type service ID is required."));
        //                }
        //                if (string.IsNullOrEmpty(updateService.ServiceName))
        //                {
        //                    return BadRequest(new ApiResponseStatus(400, "Service name is required."));
        //                }
        //                var result = await _serviceService.UpdateService(updateService);
        //                if (result)
        //                {
        //                    return Ok();
        //                }
        //                return BadRequest(new ApiResponseStatus(400, "Failed to update service"));
        //            }
        //            catch (ServiceException ex)
        //            {
        //                return BadRequest(new ApiResponseStatus(400, ex.Message));
        //            }
        //            catch (Exception ex)
        //            {
        //                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
        //            }
        //        }
    }
}
