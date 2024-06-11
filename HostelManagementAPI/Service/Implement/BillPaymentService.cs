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


            if (contract == null || contract?.Status != (int)ContractStatusEnum.pending_deposit || contract?.DepositFee == null || contract?.StudentAccountID != accountId)
            {
                throw new ServiceException("The contract is not suitable for deposited");
            }

            var billpayment = new BillPaymentDto
            {
                ContractId = contract.ContractID,
                BillAmount = contract.DepositFee,
                TotalAmount = contract.DepositFee,
                BillType = (int)BillType.Deposit,
                CreatedDate = DateTime.Now,
                TnxRef = DateTime.Now.Ticks.ToString(),
            };

            billpayment = await _billPaymentRepository.CreateBillPayment(billpayment);

            return billpayment;
        }
    }
}
