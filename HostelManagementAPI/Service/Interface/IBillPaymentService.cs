using DTOs;
using DTOs.BillPayment;

namespace Service.Interface
{
    public interface IBillPaymentService
    {
        Task CreateBillPaymentMonthly(CreateBillPaymentRequestDto createBillPaymentRequestDto);
        Task<BillPaymentDto> CreateDepositPayment(DepositRoomInputDto depositRoomInputDto, int accountId);
        Task<BillPaymentDto> ConfirmBillingTransaction(VnPayReturnUrlDto vnPayReturnUrlDto);
        Task<BillPaymentDto> GetLastMonthBillPayment(int contractId);
        Task<IEnumerable<BillPaymentDto>> GetBillPaymentsByContractId(int contractId);
        Task<BillPaymentDto> GetBillPaymentDetail(int billPaymentId);
        Task<BillPaymentDto> PrepareBillingForMonthlyPayment(int billpaymentId, int accountId);
    }
}
