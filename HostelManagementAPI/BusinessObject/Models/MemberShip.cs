namespace BusinessObject.Models
{
    public class MemberShip
    {
        public int MemberShipID { get; set; }
        public string? MemberShipName { get; set; }
        public int? CapacityHostel { get; set; }
        public int? Month { get; set; }
        public double? MemberShipFee { get; set; }
        public int? Status { get; set; }

        public IList<MemberShipRegisterTransaction> MemberShipRegisterTransactions { get; set; }
    }
}
