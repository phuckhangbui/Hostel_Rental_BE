namespace BusinessObject.Models
{
    public class BillPaymentDetail
    {
        public int BillPaymentDetailID { get; set; }
        public BillPayment BillPayment { get; set; }
        public int? BillPaymentID { get; set; }
        public RoomService? RoomService { get; set; }
        public int? RoomServiceID { get; set; }
        public double? OldNumberService { get; set; }
        public double? NewNumberService { get; set; }
        public int? Quantity { get; set; }
        public double? ServiceTotalAmount { get; set; }
    }
}
