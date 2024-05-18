namespace BusinessObject.Models
{
    public class Hostel
    {
        public int HostelID { get; set; }
        public string? HostelName { get; set; }
        public string? HostelAddress { get; set; }
        public string? HostelDescription { get; set; }
        public int? AccountID { get; set; }
        public Account OwnerAccount { get; set; }

        public IEnumerable<Room> Rooms { get; set; }
    }
}
