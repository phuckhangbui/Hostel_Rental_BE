using DTOs;
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
    public class ConfirmPaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IBillPaymentService _billPaymentService;
        private readonly IMembershipRegisterService _membershipRegisterService;
        private readonly IVnpayService _vnpayService;
        private readonly VnPayProperties _vnPayProperties;
        private readonly IAccountService _accountService;
        private readonly IContractService _contractService;

        public ConfirmPaymentController(IPaymentService paymentService, IBillPaymentService billPaymentService, IMembershipRegisterService membershipRegisterService, IVnpayService vnpayService, IOptions<VnPayProperties> vnPayProperties, IAccountService accountService, IContractService contractService)
        {
            _paymentService = paymentService;
            _billPaymentService = billPaymentService;
            _membershipRegisterService = membershipRegisterService;
            _vnpayService = vnpayService;
            _vnPayProperties = vnPayProperties.Value;
            _accountService = accountService;
            _contractService = contractService;
        }


        [HttpPost("payment/confirm")]
        [Authorize]
        public async Task<ActionResult> ConfirmPayment(VnPayReturnUrlDto vnPayReturnUrlDto)
        {
            int accountId = GetLoginAccountId();

            try
            {
                if (!_vnpayService.ConfirmReturnUrl(vnPayReturnUrlDto.Url, vnPayReturnUrlDto.TnxRef, _vnPayProperties))
                {
                    return BadRequest(new ApiResponseStatus(400, "The transaction is not valid"));
                }

                int paymentType = await _paymentService.GetPaymentTypeByTnxRef(vnPayReturnUrlDto.TnxRef);

                if (paymentType == (int)TnxPaymentType.package_register)
                {
                    await ConfirmRegisterPayment(vnPayReturnUrlDto, accountId);
                }

                else
                {
                    await ConfirmBillPayment(vnPayReturnUrlDto);
                }

                return Ok(new { paymentType = paymentType });
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

        private async Task ConfirmRegisterPayment(VnPayReturnUrlDto vnPayReturnUrlDto, int accountId)
        {
            await _membershipRegisterService.ConfirmTransaction(vnPayReturnUrlDto, accountId);

            await _accountService.UpdateAccountPackageStatus(accountId, (int)AccountPackageStatusEnum.Active);
        }

        private async Task ConfirmBillPayment(VnPayReturnUrlDto vnPayReturnUrlDto)
        {
            var billPayment = await _billPaymentService.ConfirmBillingTransaction(vnPayReturnUrlDto);

            if (billPayment.BillType == (int)BillType.Deposit)
            {
                await _contractService.ChangeContractStatus((int)billPayment.ContractId, (int)ContractStatusEnum.signed, DateTime.Now);
            }
        }
    }
}
