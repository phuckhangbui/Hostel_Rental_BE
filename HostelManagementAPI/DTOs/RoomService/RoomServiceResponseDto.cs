namespace DTOs.RoomService
{
    public class RoomServiceResponseDto
    {
        public int ServiceID { get; set; }
        public int? TypeServiceID { get; set; }
        public string ServiceName { get; set; }
        public double? ServicePrice { get; set; }
        public int Status { get; set; }
    }
}
