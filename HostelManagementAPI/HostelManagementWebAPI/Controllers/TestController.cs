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
        private readonly IVnpayService _vnpayService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;

        public TestController(ILogger<TestController> logger, IAccountService accountService, IAccountRepository accountRepository, IOptions<VnPayProperties> vnPayProperties, IVnpayService vnpayService, IFirebaseMessagingService firebaseMessagingService)
        {
            _logger = logger;
            _accountService = accountService;
            _accountRepository = accountRepository;
            _vnPayProperties = vnPayProperties.Value;
            _vnpayService = vnpayService;
            _firebaseMessagingService = firebaseMessagingService;
        }

        [HttpGet("vnpayUrl/{TnxRef}")]
        public async Task<ActionResult<string>> TestCreatePayment(string TnxRef)
        {

            string paymentUrl = _vnpayService.CreateVnpayPaymentLink("111111", 500000, "https://localhost:7050", "test hui", _vnPayProperties);
            return Ok(paymentUrl);
        }

        [HttpPost("pushnoti")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            try
            {
                string response = await _firebaseMessagingService.SendPushNotification(request.RegistrationToken, request.Title, request.Body, request.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public class NotificationRequest
        {
            public string RegistrationToken { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public Dictionary<string, string> Data { get; set; }
        }

    }
}
