using DTOs.BillPayment;

namespace Service.Interface
{
    public interface IBillPaymentService
    {
        Task CreateBillPaymentMonthly(CreateBillPaymentRequestDto createBillPaymentRequestDto);
    }
}
