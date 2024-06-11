using DTOs.BillPayment;
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

        [HttpPost("/deposit")]
        [Authorize(Policy = "User")]
        public async Task<ActionResult> DepositRoom(DepositRoomInputDto depositRoomInputDto)
        {
            int accountId = GetLoginAccountId();
            try
            {
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

    }
}
