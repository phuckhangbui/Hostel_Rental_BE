namespace BusinessObject
{
    public class MemberShipRegisterTransaction
    {
        public int MemberShipTransactionID { get; set; }
        public MemberShip MemberShip { get; set; }
        public Account OwnerAccount { get; set; }
        public DateTime? DateRegister { get; set; }
        public DateTime? DateExpire { get; set; }
        public int Status { get; set; }
    }
}
