namespace BusinessObject.Models
{
    public class ContractMember
    {
        public int ContractMemberID { get; set; }
        public Contract Contract { get; set; }
        public int? ContractID { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? CitizenCard { get; set; }
    }
}
