using DTOs.BillPayment;
﻿using DTOs;

namespace Service.Interface
{
    public interface IBillPaymentService
    {
        Task CreateBillPaymentMonthly(CreateBillPaymentRequestDto createBillPaymentRequestDto);
        Task<BillPaymentDto> CreateDepositPayment(DepositRoomInputDto depositRoomInputDto, int accountId);
        Task<BillPaymentDto> ConfirmDepositTransaction(VnPayReturnUrlDto vnPayReturnUrlDto);
        Task<BillPaymentDto> GetLastMonthBillPayment(int contractId);
    }
}
