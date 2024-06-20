using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models
{
    public class Complain
    {
        [Key]
        public int ComplainID { get; set; }
        public Account? ComplainAccount { get; set; }
        public int? AccountID { get; set; }
        public Room? Room { get; set; }
        public int? RoomID { get; set; }
        public string? ComplainText { get; set; }
        public string? ComplainResponse { get; set; }
        public DateTime? DateComplain { get; set; }
        public int? Status { get; set; }  //1 send, 2 processing, 3 resolved 
        public DateTime? DateUpdate { get; set; }
    }
}
