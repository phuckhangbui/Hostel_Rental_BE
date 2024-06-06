namespace BusinessObject.Models
{
    public class HostelImage
    {
        public int HostelImageID { get; set; }
        public string? ImageURL { get; set; }
        public Hostel? Hostel { get; set; }
        public int HostelID { get; set; }
    }
}
