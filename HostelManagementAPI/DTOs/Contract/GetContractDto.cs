namespace DTOs.Contract
{
    public class GetContractDto
    {
        public int ContractID { get; set; }
        public int OwnerAccountId { get; set; }
        public string OwnerAccountName { get; set; }
        public int? StudentAccountID { get; set; }
        public string StudentLeadAccountName { get; set; }
        public int? RoomID { get; set; }
        public string RoomName { get; set; }
        public string HostelName { get; set; }
        public string? ContractTerm { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateSign { get; set; }
        public int Status { get; set; }
        public Double? RoomFee { get; set; }
        public Double? DepositFee { get; set; }
        public List<GetContractDetailsDto> ContractMemberDetails { get; set; }
    }
}
