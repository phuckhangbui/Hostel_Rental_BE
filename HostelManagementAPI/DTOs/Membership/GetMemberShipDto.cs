namespace DTOs.Membership
{
    public class GetMemberShipDto
    {
        public int MemberShipID { get; set; }
        public string? MemberShipName { get; set; }
        public int? CapacityHostel { get; set; }
        public int? Month { get; set; }
        public double? MemberShipFee { get; set; }
        public int? Status { get; set; }
    }
}
