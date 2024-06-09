namespace BusinessObject.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string? RoomName { get; set; }
        public int? Capacity { get; set; }
        public double? Lenght { get; set; }
        public double? Width { get; set; }
        public double? Area { get; set; }
        public string? Description { get; set; }
        public double? RoomFee { get; set; }
        public Hostel Hostel { get; set; }
        public int? HostelID { get; set; }
        public int Status { get; set; }

        public IList<RoomImage> RoomImages { get; set; }
        public IList<Complain> Complains { get; set; }
        public IList<Contract> RoomContract { get; set; }
        public IList<RoomService> RoomServices { get; set; }
        public IList<RoomAppointment> RoomAppointments { get; set; }
    }
}
