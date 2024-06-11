using DTOs.BillPayment;

namespace Repository.Interface
{
    public interface IBillPaymentRepository
    {
        Task<BillPaymentDto> GetBillPaymentById(int id);
        Task<BillPaymentDto> GetBillPaymentByTnxRef(string tnxRef);
        Task<BillPaymentDto> CreateBillPayment(BillPaymentDto billPaymentDto);
        Task<BillPaymentDto> UpdateBillPayment(BillPaymentDto billPaymentDto);
    }
}
