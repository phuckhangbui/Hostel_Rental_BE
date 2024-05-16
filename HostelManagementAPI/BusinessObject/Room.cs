namespace BusinessObject
{
    public class Room
    {
        public int RoomID { get; set; }
        public string? RoomName { get; set; }
        public int? Capacity { get; set; }
        public double? Lenght { get; set; }
        public double? Width { get; set; }
        public string? Description { get; set; }
        public double? RoomFee { get; set; }
        public Hostel Hostel {  get; set; }
        public int? HostelID { get; set; }
        public int Status { get; set; }

        public IEnumerable<RoomImage> RoomImages { get; set; }
        public IEnumerable<Complain> Complains { get; set; }
        public IEnumerable<Contract> RoomContract { get; set; }
    }
}
