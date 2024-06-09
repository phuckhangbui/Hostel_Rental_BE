using Service.Vnpay;

namespace Service.Interface
{
    public interface IVnpayService
    {
        string CreateVnpayPaymentLink(string txnRef, double amount, string returnUrl, string orderInfo, VnPayProperties vnPayProperties);
    }
}
