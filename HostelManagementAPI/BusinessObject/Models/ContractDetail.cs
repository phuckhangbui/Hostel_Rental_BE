namespace BusinessObject.Models
{
    public class ContractDetail
    {
        public int ContractDetailID { get; set; }
        public Contract Contract { get; set; }
        public int? ContractID { get; set; }
        public Services Service { get; set; }
        public int? ServiceID { get; set; }
    }
}
