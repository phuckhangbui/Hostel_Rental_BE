using DTOs;
using DTOs.BillPayment;

namespace Service.Interface
{
    public interface IBillPaymentService
    {
        Task<BillPaymentDto> CreateDepositPayment(DepositRoomInputDto depositRoomInputDto, int accountId);
        Task<BillPaymentDto> ConfirmDepositTransaction(VnPayReturnUrlDto vnPayReturnUrlDto);
    }
}
