namespace BusinessObject.Models
{
    public class Hostel
    {
        public int HostelID { get; set; }
        public string? HostelName { get; set; }
        public string? HostelAddress { get; set; }
        public string? HostelDescription { get; set; }
        public int? AccountID { get; set; }
        public string? HostelType { get; set; }
        public DateTime? CreateDate { get; set; }
        public Account OwnerAccount { get; set; }
        public int? Status { get; set; }
        public IList<Room>? Rooms { get; set; }
        public IList<HostelImage>? Images { get; set; }
    }
}
