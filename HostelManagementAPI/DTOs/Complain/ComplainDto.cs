using System.ComponentModel.DataAnnotations;

namespace DTOs.Complain
{
    public class ComplainDto
    {
        [Key]
        public int? ComplainID { get; set; }
        public int? AccountID { get; set; }
        public string? AccountComplainName { get; set; }
        public int? RoomID { get; set; }
        public string? RoomName { get; set; }
        public string? ComplainText { get; set; }
        public DateTime? DateComplain { get; set; }
        public string? ComplainResponse { get; set; }
        public int? Status { get; set; }  //1 send, 2 processing, 3 resolved 
        public DateTime? DateUpdate { get; set; }
        public int? OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string? HostelName { get; set; }
    }
}
