using DTOs.BillPayment;
using DTOs.Contract;
using DTOs.Room;

namespace Repository.Interface
{
    public interface IBillPaymentRepository
    {
        Task CreateBillPaymentMonthly (
            RoomDetailResponseDto hiredRoomDto,
            GetContractDto currentContract,
            CreateBillPaymentRequestDto createBillPaymentRequestDto,
            DateTime billingMonth);
        Task<BillPaymentDto> GetCurrentMonthBillPayment(int contractId, int month, int year);
        Task<BillPaymentDto> GetLastMonthBillPayment(int contractId, int roomId);
        Task<BillPaymentDto> GetBillPaymentById(int id);
        Task<BillPaymentDto> GetBillPaymentByTnxRef(string tnxRef);
        Task<BillPaymentDto> CreateBillPayment(BillPaymentDto billPaymentDto);
        Task<BillPaymentDto> UpdateBillPayment(BillPaymentDto billPaymentDto);
        Task<IEnumerable<BillPaymentDto>> GetBillPaymentsByContractId(int contractId);
    }
}
