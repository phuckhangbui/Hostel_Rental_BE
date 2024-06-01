using DTOs.Contract;
using DTOs.Hostel;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class ContractController: BaseApiController
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet("contracts")]
        public async Task<ActionResult> GetContracts()
        {
            try
            {
                var contracts = await _contractService.GetContracts();
                return Ok(contracts);
            } catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpPut("contracts")]
        public async Task<ActionResult> Update([FromBody] UpdateContractDto contractDto)
        {
            try
            {
                await _contractService.UpdateContract(contractDto);
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

        [HttpPost("contracts")]
        public async Task<ActionResult> Create([FromBody] CreateContractDto contractDto)
        {
            try
            {
                await _contractService.CreateContract(contractDto);
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

        [HttpPut("contracts/{contractId}/status")]
        public async Task<ActionResult> ChangeContractStatus(int contractId, [FromBody] int status)
        {
            try
            {
                await _contractService.ChangeContractStatus(contractId, status);
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
