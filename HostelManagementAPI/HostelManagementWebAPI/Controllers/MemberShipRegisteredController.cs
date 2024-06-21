using DTOs.Membership;
using DTOs.MemberShipRegisterTransaction;
using HostelManagementWebAPI.MessageStatusResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Exceptions;
using Service.Interface;
using Service.Vnpay;

namespace HostelManagementWebAPI.Controllers
{
    public class MemberShipRegisteredController : BaseApiController
    {
        private readonly IMembershipRegisterService _memberShipRegisteredService;
        private readonly IAccountService _accountService;
        private readonly IVnpayService _vnpayService;
        private readonly VnPayProperties _vnPayProperties;

        public MemberShipRegisteredController(IMembershipRegisterService memberShipRegisteredService, IAccountService accountService, IVnpayService vnpayService,
            IOptions<VnPayProperties> vnPayProperties)
        {
            _memberShipRegisteredService = memberShipRegisteredService;
            _accountService = accountService;
            _vnpayService = vnpayService;
            _vnPayProperties = vnPayProperties.Value;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("admin/memberships")]
        public async Task<ActionResult> GetAllMembership()
        {
            try
            {
                var membershipRegistered = await _accountService.GetAllMemberShip();
                return Ok(membershipRegistered);
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

        [Authorize(Policy = "Admin")]
        [HttpGet("admin/memberships/detail/{accountId}")]
        public async Task<ActionResult> GetDetailMemberShipRegister(int accountId)
        {
            try
            {
                var membershipRegistered = await _memberShipRegisteredService.GetAllMembershipPackageInAccount(accountId);
                return Ok(membershipRegistered);
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

        [Authorize(Policy = "Admin")]
        [HttpGet("admin/memberships/detail/information/{accountId}")]
        public async Task<ActionResult> GetDetailMemberShipRegisterInformation(int accountId)
        {
            try
            {
                var membershipRegistered = await _accountService.GetDetailMemberShipRegisterInformation(accountId);
                return Ok(membershipRegistered);
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

        [Authorize(Policy = "Owner")]
        [HttpPost("memberships/register")]
        public async Task<ActionResult> RegisterMemberShip(RegisterMemberShipDto registerMemberShipDto)
        {
            try
            {
                var membershipRegistered = await _memberShipRegisteredService.RegisterMembership(registerMemberShipDto);

                string paymentUrl = _vnpayService.CreateVnpayPaymentLink(membershipRegistered.TnxRef, (double)membershipRegistered.PackageFee, registerMemberShipDto.ReturnUrl, "Register package", _vnPayProperties);

                return Ok(new
                {
                    paymentUrl,
                    membershipRegistered
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

        [Authorize(Policy = "Owner")]
        [HttpPost("memberships/extend")]
        public async Task<ActionResult> ExtendMemberShip(RegisterMemberShipDto registerMemberShipDto)
        {
            try
            {
                var membershipRegistered = await _memberShipRegisteredService.ExtendMembership(registerMemberShipDto);

                string paymentUrl = _vnpayService.CreateVnpayPaymentLink(membershipRegistered.TnxRef, (double)membershipRegistered.PackageFee, registerMemberShipDto.ReturnUrl, "Register package", _vnPayProperties);

                return Ok(new
                {
                    paymentUrl,
                    membershipRegistered
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

        [Authorize(Policy = "Owner")]
        [HttpPost("memberships/update")]
        public async Task<ActionResult> UpdateMemberShip(MembershipUpdatePackageDto membershipUpdatePackageDto)
        {
            try
            {
                var membershipRegistered = await _memberShipRegisteredService.UpdateMembership(membershipUpdatePackageDto);

                string paymentUrl = _vnpayService.CreateVnpayPaymentLink(membershipRegistered.TnxRef, (double)membershipRegistered.PackageFee, membershipUpdatePackageDto.ReturnUrl, "Register package", _vnPayProperties);

                return Ok(new
                {
                    paymentUrl,
                    membershipRegistered
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

        [Authorize(Policy = "Owner")]
        [HttpGet("memberships/current-active")]
        public async Task<ActionResult<MemberShipRegisterTransactionDto>> GetCurrentActiveMembership()
        {
            try
            {
                int accountId = GetLoginAccountId();
                var membership = await _memberShipRegisteredService.GetCurrentActiveMembership(accountId);

                return Ok(membership);
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

        [Authorize(Policy = "Owner")]
        [HttpGet("memberships/history")]
        public async Task<ActionResult> GetDetailMemberShipRegister()
        {
            try
            {
                int accountId = GetLoginAccountId();
                var membershipRegistered = await _memberShipRegisteredService.GetAllMembershipPackageInAccount(accountId);
                return Ok(membershipRegistered);
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

        //[Authorize(Policy = "Owner")]
        //[HttpPost("memberships/register/confirm-payment")]
        //public async Task<ActionResult> ConfirmRegisterPayment(VnPayReturnUrlDto vnPayReturnUrlDto)
        //{
        //    int accountId = GetLoginAccountId();

        //    try
        //    {
        //        if (!_vnpayService.ConfirmReturnUrl(vnPayReturnUrlDto.Url, vnPayReturnUrlDto.TnxRef, _vnPayProperties))
        //        {
        //            return BadRequest(new ApiResponseStatus(400, "The transaction is not valid"));
        //        }

        //        await _memberShipRegisteredService.ConfirmTransaction(vnPayReturnUrlDto);

        //        await _accountService.UpdateAccountPackageStatus(accountId, (int)AccountPackageStatusEnum.Active);
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

    }
}
