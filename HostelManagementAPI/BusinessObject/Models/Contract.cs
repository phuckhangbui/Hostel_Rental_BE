namespace BusinessObject.Models
{
    public class Contract
    {
        public int ContractID { get; set; }
        public Account OwnerAccount { get; set; }
        public int? OwnerAccountID { get; set; }
        public Account StudentLeadAccount { get; set; }
        public int? StudentAccountID { get; set; }
        public Room Room { get; set; }
        public int? RoomID { get; set; }
        public string? ContractTerm { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateSign { get; set; }
        public Double? RoomFee { get; set; }
        public Double? DepositFee { get; set; }

        public int Status { get; set; }

        public IList<ContractMember> Members { get; set; }
    }
}
