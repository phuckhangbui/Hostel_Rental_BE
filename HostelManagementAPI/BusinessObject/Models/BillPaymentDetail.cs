namespace BusinessObject.Models
{
    public class BillPaymentDetail
    {
        public int BillPaymentDetailID { get; set; }
        public BillPayment BillPayment { get; set; }
        public int? BillPaymentID { get; set; }
        public Service Service { get; set; }
        public int? ServiceID { get; set; }
        public int? OldNumberService { get; set; }
        public int? NewNumberService { get; set; }
        public int? Quantity { get; set; }
        public double? ServiceTotalAmount { get; set; }
    }
}
