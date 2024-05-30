
using System.ComponentModel.DataAnnotations;

namespace DTOs.Contract
{
    public class UpdateContractDto
    {
        [Required]
        public int ContractID { get; set; }
        public int OwnerAccountId { get; set; }
        public int? StudentAccountID { get; set; }
        public int? RoomID { get; set; }
        public string? ContractTerm { get; set; }
        public string? DateEnd { get; set; }
        public string? DateSign { get; set; }
        public int Status { get; set; }
        public Double? RoomFee { get; set; }
        public Double? DepositFee { get; set; }
    }
}
