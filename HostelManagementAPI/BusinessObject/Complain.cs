namespace BusinessObject
{
    public class Complain
    {
        public int ComplainID { get; set; }
        public Account ComplainAccount { get; set; }
        public Room Room { get; set; }
        public string? ComplainText { get; set;}
        public DateTime? DateComplain { get; set; }
    }
}
