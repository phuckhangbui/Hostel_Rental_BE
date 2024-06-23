using DTOs.RoomService;

namespace DTOs.Contract
{
    public class GetContractDto
    {
        public int ContractID { get; set; }
        public int OwnerAccountId { get; set; }
        public string OwnerAccountName { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerCitizen { get; set; }
        public int? StudentAccountID { get; set; }
        public string StudentLeadAccountName { get; set; }
        public string StudentLeadPhone { get; set; }
        public string StudentLeadCitizen { get; set; }
        public int? RoomID { get; set; }
        public string RoomName { get; set; }
        public string RoomDescription { get; set; }
        public string HostelName { get; set; }
        public string HostelAddress { get; set; }
        public string? ContractTerm { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateSign { get; set; }
        public int Status { get; set; }
        public Double? RoomFee { get; set; }
        public Double? DepositFee { get; set; }
        public double? InitWaterNumber { get; set; }
        public double? InitElectricityNumber { get; set; }
        public List<RoomServiceResponseForContractDto> RoomServiceDetails { get; set; }
        public List<GetContractDetailsDto> ContractMemberDetails { get; set; }
    }
}
