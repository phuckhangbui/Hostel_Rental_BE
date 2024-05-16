namespace BusinessObject
{
    public class ContractMember
    {
        public int ContractMemberD {  get; set; }
        public Contract Contract {  get; set; }
        public int? ContractID { get; set; }
        public Account Student { get; set; }
        public int? AccountID { get; set; }
    }
}
