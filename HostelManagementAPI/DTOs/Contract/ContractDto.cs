
namespace DTOs.Contract
{
    public class ContractDto
    {
        public int ContractID { get; set; }
        public int? AccountID { get; set; }
        public int? RoomID { get; set; }
        public string? ContractTerm { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateSign { get; set; }
        public int Status { get; set; }
    }
}
