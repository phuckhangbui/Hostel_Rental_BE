namespace BusinessObject.Models
{
    public class RoomService
    {
        public int RoomServiceId { get; set; }
        public int RoomId { get; set; }
        public int Status { get; set; }
        public Room? Room { get; set; }
        public TypeService? TypeService { get; set; }
        public int TypeServiceId { get; set; }
        public double? Price { get; set; }
        public bool? IsSelected { get; set; }
        public IList<BillPaymentDetail> BillPaymentDetails { get; set; }


    }
}
