namespace BusinessObject
{
    public class Notice
    {
        public int NoticeID { get; set; }
        public Account NoticeAccount { get; set; }
        public Account ReceiveAccount { get; set; }
        public string? NoticeText{ get; set; }
        public DateTime? DateNotice { get; set; }
    }
}
