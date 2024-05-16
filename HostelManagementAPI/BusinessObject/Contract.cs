namespace BusinessObject
{
    public class Contract
    {
        public int ContractID { get; set; }
        public Account OwnerAccount { get; set; }
        public Account StudentLeadAccount { get; set; }
        public Room Room { get; set; }
        public string? ContractTerm { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set;}
        public DateTime? DateSign { get; set; }
        public int Status { get; set; }

        public IEnumerable<ContractMember> Members { get; set; }
        public IEnumerable<ContractDetail> ContractDetails { get; set; }
    }
}
