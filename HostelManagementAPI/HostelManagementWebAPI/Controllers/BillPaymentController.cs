﻿using DTOs;
using DTOs.BillPayment;
using DTOs.Enum;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Exceptions;
using Service.Interface;
using Service.Vnpay;

namespace HostelManagementWebAPI.Controllers
{

    public class BillPaymentController : BaseApiController
    {
        private readonly IBillPaymentService _billPaymentService;
        private readonly IContractService _contractService;
        private readonly IVnpayService _vnpayService;
        private readonly VnPayProperties _vnPayProperties;

        public BillPaymentController(IBillPaymentService billPaymentService, IContractService contractService, IOptions<VnPayProperties> vnPayProperties, IVnpayService vnpayService)
        {
            _billPaymentService = billPaymentService;
            _contractService = contractService;
            _vnPayProperties = vnPayProperties.Value;
            _vnpayService = vnpayService;
        }

        [Authorize(Policy = "Member")]
        [HttpPost("deposit")]
        public async Task<ActionResult> DepositRoom(DepositRoomInputDto depositRoomInputDto)
        {
            int accountId = GetLoginAccountId();
            try
            {
                var contract = await _contractService.GetContractDetailByContractId(depositRoomInputDto.ContractId);

                if (contract == null || contract?.Status != (int)ContractStatusEnum.pending || contract?.DepositFee == null || contract?.StudentAccountID != accountId)
                {
                    throw new ServiceException("The contract is not suitable for deposited");
                }

                var billPayment = await _billPaymentService.CreateDepositPayment(depositRoomInputDto, accountId);

                string paymentUrl = _vnpayService.CreateVnpayPaymentLink(billPayment.TnxRef, (double)billPayment.TotalAmount, depositRoomInputDto.ReturnUrl, "Room deposit", _vnPayProperties);

                return Ok(new
                {
                    paymentUrl,
                    billPayment
                });
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

        [Authorize(Policy = "Member")]
        [HttpPost("deposit/confirm-payment")]
        public async Task<ActionResult> ConfirmRegisterPayment(VnPayReturnUrlDto vnPayReturnUrlDto)
        {
            int accountId = GetLoginAccountId();

            try
            {
                if (!_vnpayService.ConfirmReturnUrl(vnPayReturnUrlDto.Url, vnPayReturnUrlDto.TnxRef, _vnPayProperties))
                {
                    return BadRequest(new ApiResponseStatus(400, "The transaction is not valid"));
                }

                var billPayment = await _billPaymentService.ConfirmDepositTransaction(vnPayReturnUrlDto);

                await _contractService.ChangeContractStatus((int)billPayment.ContractId, (int)ContractStatusEnum.signed);

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
