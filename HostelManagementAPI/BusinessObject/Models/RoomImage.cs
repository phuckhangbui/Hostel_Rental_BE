namespace BusinessObject.Models
{
    public class RoomImage
    {
        public int RoomImgID { get; set; }
        public string? RoomUrl { get; set; }
        public Room Room { get; set; }
        public int? RoomID { get; set; }
    }
}
