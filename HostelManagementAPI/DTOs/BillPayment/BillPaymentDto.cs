namespace DTOs.BillPayment
{
    public class BillPaymentDto
    {
        public int? BillPaymentID { get; set; }
        public int? ContractId { get; set; }
        public double? BillAmount { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public DateTime? CreatedDate { get; set; }
        public double? TotalAmount { get; set; }
        public int? BillPaymentStatus { get; set; }
        public int? BillType { get; set; }
        public string? TnxRef { get; set; }

        //public IList<BillPaymentDetail> Details { get; set; }
    }
}
