namespace DTOs.BillPayment
{
    public class BillPaymentHistoryMember
    {
        public int BillPaymentId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public int? BillType { get; set; }
        public double? BillAmount { get; set; }
        public double? TotalAmount { get; set; }
    }
}
