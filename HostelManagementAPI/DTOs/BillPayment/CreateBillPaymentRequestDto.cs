namespace DTOs.BillPayment
{
    public class CreateBillPaymentRequestDto
    {
        public int ContractId { get; set; }
        public int BillType { get; set; }
        public List<ServiceReadingDto> ServiceReadings { get; set; }
    }

    public class ServiceReadingDto
    {
        public int RoomServiceId { get; set; }
        public double NewNumberService { get; set; }
    }
}
