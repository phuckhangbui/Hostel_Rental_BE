using DTOs.Enum;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class PaymentService : IPaymentService
    {
        private readonly IMembershipRegisterRepository _membershipRegisterRepository;
        private readonly IBillPaymentRepository _billPaymentRepository;

        public PaymentService(IMembershipRegisterRepository membershipRegisterRepository, IBillPaymentRepository billPaymentRepository)
        {
            _membershipRegisterRepository = membershipRegisterRepository;
            _billPaymentRepository = billPaymentRepository;
        }

        public async Task<int> GetPaymentTypeByTnxRef(string tnxRef)
        {
            var memberShipRegister = await _membershipRegisterRepository.GetMembershipTransactionBaseOnTnxRef(tnxRef);

            if (memberShipRegister != null)
            {
                return (int)TnxPaymentType.package_register;
            }

            var billPayment = await _billPaymentRepository.GetBillPaymentByTnxRef(tnxRef);
            if (billPayment != null)
            {
                if (billPayment.BillType == (int)BillType.Deposit)
                {
                    return (int)TnxPaymentType.deposit;
                }
                return (int)TnxPaymentType.bill_payment;
            }

            throw new ServiceException("This TnxRef does not match with any transaction");
        }
    }
}
