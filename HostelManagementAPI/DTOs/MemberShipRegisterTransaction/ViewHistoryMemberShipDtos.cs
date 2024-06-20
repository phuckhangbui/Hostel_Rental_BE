namespace DTOs.MemberShipRegisterTransaction
{
    public class ViewHistoryMemberShipDtos
    {
        public int MemberShipTransactionID { get; set; }
        public string MembershipName { get; set; }
        public int CapacityHostel { get; set; }
        public int Month { get; set; }
        public double PackageFee { get; set; }
        public DateTime? DateRegister { get; set; }
        public DateTime? DateExpire { get; set; }
        public int? Status { get; set; }
    }
}
