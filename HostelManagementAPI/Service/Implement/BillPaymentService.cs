using DTOs;
using DTOs.BillPayment;
using DTOs.Enum;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class BillPaymentService : IBillPaymentService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IBillPaymentRepository _billPaymentRepository;

        public BillPaymentService(IContractRepository contractRepository, IBillPaymentRepository billPaymentRepository)
        {
            _contractRepository = contractRepository;
            _billPaymentRepository = billPaymentRepository;
        }

        public async Task<BillPaymentDto> CreateDepositPayment(DepositRoomInputDto depositRoomInputDto, int accountId)
        {
            var contract = await _contractRepository.GetContractById(depositRoomInputDto.ContractId);



            var billpayment = new BillPaymentDto
            {
                ContractId = contract.ContractID,
                BillAmount = contract.DepositFee,
                TotalAmount = contract.DepositFee,
                BillType = (int)BillType.Deposit,
                BillPaymentStatus = (int)BillPaymentStatus.Pending,
                CreatedDate = DateTime.Now,
                TnxRef = DateTime.Now.Ticks.ToString(),
            };

            billpayment = await _billPaymentRepository.CreateBillPayment(billpayment);

            return billpayment;
        }

        public async Task<BillPaymentDto> ConfirmDepositTransaction(VnPayReturnUrlDto vnPayReturnUrlDto)
        {
            var billPayment = await _billPaymentRepository.GetBillPaymentByTnxRef(vnPayReturnUrlDto.TnxRef);

            if (billPayment == null)
            {
                throw new ServiceException("No bill match");
            }


            if (vnPayReturnUrlDto == null)
            {
                throw new ServiceException("No transaction match");
            }

            if (billPayment.BillPaymentStatus != (int)BillPaymentStatus.Pending)
            {
                throw new ServiceException("Billpayment has been paid");
            }

            billPayment.BillPaymentStatus = (int)BillPaymentStatus.Paid;

            await _billPaymentRepository.UpdateBillPayment(billPayment);

            return billPayment;
        }
    }
}
