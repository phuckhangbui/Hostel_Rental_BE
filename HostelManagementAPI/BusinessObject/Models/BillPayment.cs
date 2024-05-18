namespace BusinessObject.Models
{
    public class BillPayment
    {
        public int BillPaymentID { get; set; }
        public Contract Contract { get; set; }
        public double? BillAmount { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public DateTime? CreatedDate { get; set; }
        public double? TotalAmount { get; set; }
        public int? BillPaymentStatus { get; set; }

        public IEnumerable<BillPaymentDetail> Details { get; set; }
    }
}
