
using System.ComponentModel.DataAnnotations;

namespace DTOs.Contract
{
    public class ContractDto
    {
        [Required]
        public int ContractID { get; set; }
        public int OwnerAccount { get; set; }
        public int? AccountID { get; set; }
        public int? RoomID { get; set; }
        public string? ContractTerm { get; set; }
        public string? CreatedDate { get; set; }
        public string? DateStart { get; set; }
        public string? DateEnd { get; set; }
        public string? DateSign { get; set; }
        public int Status { get; set; }
    }
}
