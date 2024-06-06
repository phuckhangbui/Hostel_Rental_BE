using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Repository.Interface;
using Service.Interface;
using Service.Vnpay;

namespace HostelManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;
        private readonly VnPayProperties _vnPayProperties;

        public TestController(ILogger<TestController> logger, IAccountService accountService, IAccountRepository accountRepository, IOptions<VnPayProperties> vnPayProperties)
        {
            _logger = logger;
            _accountService = accountService;
            _accountRepository = accountRepository;
            _vnPayProperties = vnPayProperties.Value;
        }

        [HttpGet("vnpayUrl/{TnxRef}")]
        public async Task<ActionResult<string>> TestCreatePayment(string TnxRef)
        {
            VnpayPaymentUrlEntity vnpay = new VnpayPaymentUrlEntity();
            vnpay.Url = _vnPayProperties.Url;
            vnpay.TmnCode = _vnPayProperties.TmnCode;
            vnpay.Version = _vnPayProperties.Version;
            vnpay.HashSecret = _vnPayProperties.HashSecret;

            vnpay.Amount = 100000;
            vnpay.CreateDate = DateTime.Now;
            vnpay.ExpireDate = DateTime.Now.AddMinutes(10);
            vnpay.Locale = "vn";
            vnpay.OrderInfo = "test vnpay";
            vnpay.ReturnUrl = "https://localhost:44383/test";
            vnpay.TxnRef = TnxRef;

            string paymentUrl = VnpayUrlMaker.CreateUrl(vnpay, HttpContext);
            return Ok(paymentUrl);
        }
    }
}
