namespace DTOs.BillPayment
{
    public class BillPaymentDetailResponseDto
    {
        public int BillPaymentDetailID { get; set; }
        public int? RoomServiceID { get; set; }
        public double? OldNumberService { get; set; }
        public double? NewNumberService { get; set; }
        public int? Quantity { get; set; }
        public double? ServiceTotalAmount { get; set; }

        public double? ServicePrice { get; set; }
        public string? ServiceType { get; set; }
        public string? ServiceUnit { get; set; }
    }
}
