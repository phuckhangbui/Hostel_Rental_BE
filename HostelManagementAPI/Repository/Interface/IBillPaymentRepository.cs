using BusinessObject.Models;
using DTOs.BillPayment;

namespace Repository.Interface
{
    public interface IBillPaymentRepository
    {
        Task CreateBillPaymentMonthly (BillPayment billPayment);
        Task<BillPayment> GetCurrentMonthBillPayment(int contractId, int month, int year);
        Task<BillPaymentDetail> GetLastBillPaymentDetail(int roomServiceId);
        Task<BillPaymentDto> GetBillPaymentById(int id);
        Task<BillPaymentDto> GetBillPaymentByTnxRef(string tnxRef);
        Task<BillPaymentDto> CreateBillPayment(BillPaymentDto billPaymentDto);
        Task<BillPaymentDto> UpdateBillPayment(BillPaymentDto billPaymentDto);
    }
}
