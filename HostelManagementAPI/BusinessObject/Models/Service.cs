namespace BusinessObject.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public TypeService TypeService { get; set; }
        public int? TypeServiceID { get; set; }
        public string? ServiceName { get; set; }
        public double? ServicePrice { get; set; }

        public IEnumerable<BillPaymentDetail> BillPaymentDetail { get; set; }
        public IEnumerable<ContractDetail> ContractDetails { get; set; }
    }
}
