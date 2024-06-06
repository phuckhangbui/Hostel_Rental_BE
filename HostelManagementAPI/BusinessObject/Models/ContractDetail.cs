namespace BusinessObject.Models
{
    public class ContractDetail
    {
        public int ContractDetailID { get; set; }
        public Contract? Contract { get; set; }
        public int? ContractID { get; set; }
        public RoomService? RoomService { get; set; }
        public int? ServiceID { get; set; }
    }
}
