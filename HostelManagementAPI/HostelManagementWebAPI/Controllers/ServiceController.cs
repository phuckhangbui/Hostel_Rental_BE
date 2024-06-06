using BusinessObject.Models;
using DTOs.Service;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class ServiceController : BaseApiController
    {
        private readonly IServiceService _serviceService;
        private readonly ITypeServiceService _typeServiceService;
        public ServiceController(IServiceService serviceService, ITypeServiceService typeServiceService)
        {
            _serviceService = serviceService;
            _typeServiceService = typeServiceService;
        }

        [HttpPost("owner-create-service")]
        public async Task<ActionResult> CreateService([FromBody] CreateServiceDto createService)
        {
            try
            {
                if (createService == null)
                {
                       return BadRequest(new ApiResponseStatus(400, "Invalid request."));
                }
                if (createService.ServicePrice <= 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Service price must be greater than 0."));
                }
                if(createService.TypeServiceID == null)
                {
                    return BadRequest(new ApiResponseStatus(400, "Type service ID is required."));
                }
                if (string.IsNullOrEmpty(createService.ServiceName))
                {
                    return BadRequest(new ApiResponseStatus(400, "Service name is required."));
                }
                var result = await _serviceService.CreateService(createService);
                if (result)
                {
                    return Ok();
                }
                return BadRequest(new ApiResponseStatus(400, "Failed to create new service"));
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

        [HttpPut("owner-update-service")]
        public async Task<ActionResult> UpdateService([FromBody] UpdateServiceDto updateService)
        {
            try
            {                
                if (updateService == null)
                {
                    return BadRequest(new ApiResponseStatus(400, "Invalid request."));
                }
                var checkServiceExist = await _serviceService.CheckServiceExist(updateService.ServiceID);
                if (!checkServiceExist)
                {
                    return BadRequest(new ApiResponseStatus(400, "Service does not exist."));
                }
                var checkTypeServiceExist = await _typeServiceService.CheckExistTypeService(updateService.TypeServiceID);
                if (!checkTypeServiceExist)
                {
                    return BadRequest(new ApiResponseStatus(400, "Type service does not exist."));
                }
                if (updateService.ServicePrice <= 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Service price must be greater than 0."));
                }
                if (updateService.TypeServiceID == 0)
                {
                    return BadRequest(new ApiResponseStatus(400, "Type service ID is required."));
                }
                if (string.IsNullOrEmpty(updateService.ServiceName))
                {
                    return BadRequest(new ApiResponseStatus(400, "Service name is required."));
                }
                var result = await _serviceService.UpdateService(updateService);
                if (result)
                {
                    return Ok();
                }
                return BadRequest(new ApiResponseStatus(400, "Failed to update service"));
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
        [HttpGet("services")]
        public async Task<ActionResult> GetAllServices()
        {
            try
            {
                List<ServiceResponseDto> services = await _serviceService.GetServices();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpDelete("services/{serviceId}")]
        public async Task<IActionResult> RemoveService(int serviceId)
        {
            try
            {
                var checkServiceExist = await _serviceService.CheckServiceExist(serviceId);
                if (!checkServiceExist)
                {
                    return BadRequest(new ApiResponseStatus(400, "Service does not exist."));
                }
                await _serviceService.RemoveService(serviceId);
                return Ok("Delete Service with ID: " + serviceId + " complete");
            } catch (ServiceException ex)
            {
                return BadRequest(new ApiResponseStatus(400, ex.Message));
            } catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
            
        }
    }
}
