namespace BusinessObject.Models
{
    public class MemberShip
    {
        public int MemberShipID { get; set; }
        public string? MemberShipName { get; set; }
        public int? CapacityHostel { get; set; }
        public int? Month { get; set; }
        public double? MemberShipFee { get; set; }

        public IEnumerable<MemberShipRegisterTransaction> MemberShipRegisterTransactions { get; set; }
    }
}
