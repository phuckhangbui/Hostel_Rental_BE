namespace BusinessObject.Models
{
    public class TypeService
    {
        public int TypeServiceID { get; set; }
        public string? TypeName { get; set; }
        public IList<RoomService> RoomServices { get; set; }
    }
}
