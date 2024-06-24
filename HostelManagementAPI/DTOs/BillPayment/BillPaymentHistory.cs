namespace DTOs.BillPayment
{
    public class BillPaymentHistory
    {
        public int BillPaymentID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public int? BillType { get; set; }
        public double? BillAmount { get; set; }
        public double? TotalAmount { get; set; }
    }
}
