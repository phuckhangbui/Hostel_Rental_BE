namespace DTOs.MemberShipRegisterTransaction
{
    public class MemberShipRegisterTransactionDto
    {
        public int? MemberShipTransactionID { get; set; }
        public int? MemberShipID { get; set; }
        public int? AccountID { get; set; }
        public DateTime? DateRegister { get; set; }
        public DateTime? DateExpire { get; set; }
        public double? PackageFee { get; set; } // user pay this, maybe get discount so that is the reason there is 2 field
        public int? Status { get; set; }
        public string? TnxRef { get; set; }
        public string? MemberShipName { get; set; }
        public int? CapacityHostel { get; set; }
        public double? MemberShipFee { get; set; }  //nay la fee cua membership, not the amount user have to pay
        public int? Month { get; set; }

    }
}
