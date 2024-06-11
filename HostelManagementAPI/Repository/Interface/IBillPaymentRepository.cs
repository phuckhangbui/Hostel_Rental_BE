using BusinessObject.Models;
using DTOs.BillPayment;

namespace Repository.Interface
{
    public interface IBillPaymentRepository
    {
        Task CreateBillPaymentMonthly (BillPayment billPayment);
        Task<BillPayment> GetCurrentMonthBillPayment(int contractId, int month, int year);
        Task<BillPaymentDetail> GetLastBillPaymentDetail(int roomServiceId);
    }
}
