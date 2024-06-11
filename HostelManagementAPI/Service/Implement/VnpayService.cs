using Service.Interface;
using Service.Vnpay;
using System.Collections.Specialized;
using System.Web;


namespace Service.Implement
{
    public class VnpayService : IVnpayService
    {
        public string CreateVnpayPaymentLink(string txnRef, double amount, string returnUrl, string orderInfo, VnPayProperties vnPayProperties)
        {
            //Get Config Info
            string vnp_Returnurl = returnUrl; //URL nhan ket qua tra ve 
            string vnp_Url = vnPayProperties.Url; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = vnPayProperties.TmnCode; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = vnPayProperties.HashSecret; //Secret Key

            var price = (long)(amount * 100);
            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", vnPayProperties.Version);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", "https://localhost:7050");
            vnpay.AddRequestData("vnp_Locale", "vn");

            vnpay.AddRequestData("vnp_OrderInfo", orderInfo);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_TxnRef", txnRef);

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return paymentUrl;
        }

        public bool ConfirmReturnUrl(string url, string tnxRef, VnPayProperties vnPayProperties)
        {
            NameValueCollection queryParams = HttpUtility.ParseQueryString(HttpUtility.UrlDecode(url));

            Dictionary<string, string> vnpayData = queryParams.AllKeys.ToDictionary(k => k, k => queryParams[k]);
            string vnp_HashSecret = vnPayProperties.HashSecret;

            VnPayLibrary vnpay = new VnPayLibrary();

            foreach (var kvp in vnpayData)
            {
                //get all querystring data
                if (!string.IsNullOrEmpty(kvp.Key) && kvp.Key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(kvp.Key, kvp.Value);
                }
            }

            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            string vnp_SecureHash = vnpayData["vnp_SecureHash"];
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

            if (checkSignature && vnp_ResponseCode == "00" && vnp_TransactionStatus == "00" && vnpay.GetResponseData("vnp_TxnRef") == tnxRef)
            {
                return true;
            }

            return false;

        }
    }
}
