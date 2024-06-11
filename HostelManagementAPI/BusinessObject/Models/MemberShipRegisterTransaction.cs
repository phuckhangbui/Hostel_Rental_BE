namespace BusinessObject.Models
{
    public class MemberShipRegisterTransaction
    {
        public int MemberShipTransactionID { get; set; }
        public MemberShip MemberShip { get; set; }
        public int? MemberShipID { get; set; }
        public Account OwnerAccount { get; set; }
        public int? AccountID { get; set; }
        public DateTime? DateRegister { get; set; }
        public DateTime? DateExpire { get; set; }
        public double PackageFee { get; set; }
        public int? Status { get; set; }
        public string? TnxRef { get; set; }
    }
}
