namespace BusinessObject.Models
{
    public class RoomService
    {
        public int RoomId { get; set; }
        public int ServiceId { get; set; }
        public int Status { get; set; }
        public Room Room { get; set; }
        public Services Service { get; set; }
    }
}
