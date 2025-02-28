﻿using DTOs.Contract;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class ContractController : BaseApiController
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpPut("contracts")]
        public async Task<ActionResult> Update(int key, [FromBody] UpdateContractDto contractDto)
        {
            try
            {
                await _contractService.UpdateContract(key, contractDto);
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

        [Authorize(policy: "Owner")]
        [HttpPost("owner/contract/create")]
        public async Task<ActionResult> Create(CreateContractDto contractDto)
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

        //[HttpPut("contracts/{contractId}/status")]
        //public async Task<ActionResult> ChangeContractStatus(int contractId, int status)
        //{
        //    try
        //    {
        //        await _contractService.ChangeContractStatus(contractId, status);
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

        [HttpGet("owner/contracts/{ownerId}")]
        public async Task<ActionResult> GetContractsByOwnerId(int ownerId)
        {
            try
            {
                var contracts = await _contractService.GetContractsByOwnerId(ownerId);
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpGet("student/contracts/{studentId}")]
        public async Task<ActionResult> GetContractsByStudentId(int studentId)
        {
            try
            {
                var contracts = await _contractService.GetContractsByStudentId(studentId);
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpGet("contracts/getDetails/{contractId}")]
        public async Task<ActionResult> GetContractDetailsById(int contractId)
        {
            try
            {
                var contracts = await _contractService.GetContractDetailByContractId(contractId);
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
            }
        }

        [HttpPost("contracts/contractsMember")]
        public async Task<ActionResult> CreateContractMembers([FromBody] CreateListContractMemberDto createListContractMemberDto)
        {
            try
            {
                await _contractService.AddContractMember(createListContractMemberDto);
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
