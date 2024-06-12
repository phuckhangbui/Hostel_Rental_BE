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
        private readonly IBillPaymentRepository _billPaymentRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IRoomRepository _roomRepository;

        public BillPaymentService(
            IBillPaymentRepository billPaymentRepository,
            IContractRepository contractRepository,
            IRoomRepository roomRepository)
        {
            _billPaymentRepository = billPaymentRepository;
            _contractRepository = contractRepository;
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<BillPaymentDto>> GetBillPaymentsByContractId(int contractId)
        {
            var currentContract = await _contractRepository.GetContractById(contractId);
            if (currentContract == null)
            {
                throw new ServiceException("Contract not found with this ID");
            }

            return await _billPaymentRepository.GetBillPaymentsByContractId(contractId);
        }

        public async Task<BillPaymentDto> GetLastMonthBillPayment(int contractId)
        {
            var currentContract = await _contractRepository.GetContractById(contractId);
            if (currentContract == null) 
            {
                throw new ServiceException("Contract not found with this ID");
            }

            var roomId = currentContract.RoomID;

            return await _billPaymentRepository.GetLastMonthBillPayment(contractId, (int)roomId);
        }

        public async Task CreateBillPaymentMonthly(CreateBillPaymentRequestDto createBillPaymentRequestDto)
        {
            var contractId = createBillPaymentRequestDto.ContractId;

            var currentContract = await _contractRepository.GetContractById(contractId);
            if (currentContract == null)
            {
                throw new ServiceException("Contract not found with this ID");
            }
            else
            {
                var currentDate = DateTime.Now;
                var year = currentDate.Year;
                var month = currentDate.Month;

                //if (currentDate < currentContract.DateStart || currentDate > currentContract.DateEnd)
                //{
                //    throw new ServiceException("Contract is not active yet");
                //}

                int monthsSinceStart = ((currentDate.Year - currentContract.DateStart.Value.Year) * 12) +
                    currentDate.Month - currentContract.DateStart.Value.Month;
                var billingMonth = currentContract.DateStart.Value.AddMonths(monthsSinceStart);

                //Check bill exist or not
                var existingBillPayment = await _billPaymentRepository.GetCurrentMonthBillPayment(contractId, month, year);
                if (existingBillPayment != null)
                {
                    throw new ServiceException("A bill for this month already exists.");
                }

                var hiredRoom = await _roomRepository.GetRoomDetailById((int)currentContract.RoomID);
                if (hiredRoom != null)
                {
                    await _billPaymentRepository.CreateBillPaymentMonthly(hiredRoom, currentContract, createBillPaymentRequestDto, billingMonth);
                }

            }
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
