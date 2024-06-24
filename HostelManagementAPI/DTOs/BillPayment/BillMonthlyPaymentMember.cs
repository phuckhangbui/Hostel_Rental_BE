namespace DTOs.BillPayment
{
    public class BillMonthlyPaymentMember
    {
        public int BillPaymentID { get; set; }
        public int? ContractId { get; set; }
        public double? BillAmount { get; set; }
        public double? TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? BillPaymentStatus { get; set; }
        public int? RoomID { get; set; }
        public string? RoomName { get; set; }
    }
}
