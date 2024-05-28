namespace DTOs.MemberShipRegisterTransaction
{
    public class ViewMemberShipDto
    {
        public int MemberShipTransactionID { get; set; }
        public int AccountID { get; set; }
        public string Email { get; set; }
        public string MembershipName { get; set; }
        public DateTime DateRegister { get; set;}
        public DateTime DateExpire { get; set; }
        public int Status { get; set; }
    }
}
