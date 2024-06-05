namespace DTOs.MemberShipRegisterTransaction
{
    public class ViewTransactionMembership
    {
        public int MemberShipTransactionID {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double PackageFee { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
