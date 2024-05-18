namespace BusinessObject.Models
{
    public class ContractDetail
    {
        public int ContractDetailID { get; set; }
        public Contract Contract { get; set; }
        public int? ContractID { get; set; }
        public Service Service { get; set; }
        public int? ServiceID { get; set; }
    }
}
