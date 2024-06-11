using DTOs.BillPayment;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interface;

namespace HostelManagementWebAPI.Controllers
{
    [ApiController]
    public class BillPaymentController : BaseApiController
    {
        private readonly IBillPaymentService _billPaymentService;

        public BillPaymentController(IBillPaymentService billPaymentService)
        {
            _billPaymentService = billPaymentService;
        }

        [HttpPost("bill-payment")]
        public async Task<ActionResult> Create([FromBody] CreateBillPaymentRequestDto createBillPaymentRequestDto)
        {
            try
            {
                await _billPaymentService.CreateBillPaymentMonthly(createBillPaymentRequestDto);
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
