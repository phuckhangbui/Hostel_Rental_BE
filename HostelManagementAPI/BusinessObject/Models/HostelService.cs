namespace BusinessObject.Models
{
    public class HostelService
    {
        public int HostelId { get; set; }
        public int ServiceId { get; set; }
        public int Status { get; set; }
        public Hostel Hostel { get; set; }
        public Services Service { get; set; }
    }
}
