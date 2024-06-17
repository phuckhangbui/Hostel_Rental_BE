namespace DTOs.BillPayment
{
    public class CreateBillPaymentRequestDto
    {
        public List<RoomBillPaymentDto> RoomBillPayments { get; set; }
    }

    public class RoomBillPaymentDto
    {
        public int ContractId { get; set; }
        public List<ServiceReadingDto> ServiceReadings { get; set; }
    }

    public class ServiceReadingDto
    {
        public int RoomServiceId { get; set; }
        public double NewNumberService { get; set; }
    }
}
