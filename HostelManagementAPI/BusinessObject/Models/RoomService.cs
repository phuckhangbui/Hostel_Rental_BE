namespace BusinessObject.Models
{
    public class RoomService
    {
        public int RoomId { get; set; }
        public int ServiceId { get; set; }
        public int Status { get; set; }
        public Room Room { get; set; }
        public Service Service { get; set; }
    }
}
