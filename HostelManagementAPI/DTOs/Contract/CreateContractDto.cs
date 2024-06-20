using System.Diagnostics.Contracts;

namespace DTOs.Contract
{
    public class CreateContractDto
    {
        public int OwnerAccountID { get; set; }
        public int? StudentAccountID { get; set; }
        public int? RoomID { get; set; }
        public string? ContractTerm { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public double? RoomFee { get; set; }
        public double? DepositFee { get; set; }
        public double? InitWater {  get; set; }
        public double? InitElec { get; set; }
        public IEnumerable<CreateContractMemberDto> ContractMember {  get; set; }
        public IEnumerable<int> RoomService { get; set; }
    }
}
