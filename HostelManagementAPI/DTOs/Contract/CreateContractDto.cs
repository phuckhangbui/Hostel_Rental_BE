using System.Diagnostics.Contracts;

namespace DTOs.Contract
{
    public class CreateContractDto
    {
        public int OwnerAccountId { get; set; }
        public int? StudentAccountID { get; set; }
        public int? RoomID { get; set; }
        public string? ContractTerm { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateSign { get; set; }
        public int Status { get; set; }
        public double? RoomFee { get; set; }
        public double? DepositFee { get; set; }
    }
}
