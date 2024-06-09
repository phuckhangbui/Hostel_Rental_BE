namespace DTOs.MemberShipRegisterTransaction
{
    public class MemberShipRegisterTransactionDto
    {
        public int? MemberShipTransactionID { get; set; }
        public int? MemberShipID { get; set; }
        public int? AccountID { get; set; }
        public DateTime? DateRegister { get; set; }
        public DateTime? DateExpire { get; set; }
        public double? PackageFee { get; set; }
        public int? Status { get; set; }
        public string? TnxRef { get; set; }

    }
}
